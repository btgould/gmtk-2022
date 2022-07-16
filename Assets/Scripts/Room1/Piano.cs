using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    private Vector2 dim;

    [SerializeField] private int sampleRate = 44100;
    [SerializeField] private float gain = 0.1f;
    [SerializeField] private float decay = 0.99f;
    [SerializeField] private float startDelay = 10;

    private float frequency;
    private float minFrequency = 130.8128f;
    int numKeys = 65;
    private float freqStep = Mathf.Pow(2, 1.0f / 12);


    private float increment;
    private float phase = 0;
    private float gainCurr = 0;
    private float currStartDelay = 0;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gainCurr = gain;
        currStartDelay = 0;

        dim = GetComponent<Collider2D>().bounds.extents;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (audioSource.isPlaying) gainCurr *= decay;
        if (gainCurr < 1e-2)
        {
            audioSource.Stop();
            gainCurr = gain;
        }

        currStartDelay--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currStartDelay <= 0)
        {
            // Determine location hit on piano
            Vector2 pos = collision.collider.attachedRigidbody.position;
            float min = transform.position.x - dim.x;
            float max = transform.position.x + dim.x;
            float frac = (pos.x - min) / (max - min);

            int key = Mathf.FloorToInt(frac * numKeys);
            frequency = minFrequency * Mathf.Pow(freqStep, key);

            // Play sound
            gainCurr = gain;
            currStartDelay = startDelay;
            audioSource.Play();

            Debug.Log(frac);
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        // update increment in case frequency has changed
        increment = frequency * 2f * Mathf.PI / sampleRate;

        for (int i = 0; i < data.Length; i++)
        {
            phase = phase + increment;
            if (phase > 2 * Mathf.PI) phase = 0;

            data[i] = (float)(gainCurr * Mathf.Sin(phase));

            // if we have stereo, we copy the mono data to each channel
            if (channels == 2)
            {
                data[i + 1] = data[i];
                i++;
            }
        }
    }
}

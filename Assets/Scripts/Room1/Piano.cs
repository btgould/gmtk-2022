using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    private Vector2 dim;

    [SerializeField] private int sampleRate = 44100;
    [SerializeField] private int numSecs = 1;
    [SerializeField] public float frequency = 440f;
    [SerializeField] public float gain = 0.1f;
    [SerializeField] public float decay = 0.99f;


    private float increment;
    private float phase = 0;
    private float gainCurr = 0;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gainCurr = gain;
        dim = GetComponent<Collider2D>().bounds.extents;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource.isPlaying) gainCurr *= decay;
        if (gainCurr < 1e-2)
        {
            audioSource.Stop();
            gainCurr = gain;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Determine location hit on piano
        Vector2 pos = collision.collider.attachedRigidbody.position;
        float min = transform.position.x - dim.x;
        float max = transform.position.x + dim.x;
        float frac = (pos.x - min) / (max - min);

        // Play sound
        gainCurr = gain;
        audioSource.Play();

        Debug.Log(frac);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private PlayerAudio s;

    void Awake()
    {
        if (s != null)
        {
            Debug.LogError("Attempted to instantiate multiple singletons");
        }

        s = this;
    }

    [SerializeField] private List<AudioClip> m_shakes;
    public AudioClip shake
    {
        get
        {
            int idx = Random.Range(0, m_shakes.Count);
            return m_shakes[idx];
        }
    }

    public PlayerAudio get() { return s; }
}

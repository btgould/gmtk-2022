using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    private static CarAudio s;

    void Awake()
    {
        if (s != null)
        {
            Debug.LogError("Attempted to instantiate multiple singletons");
        }

        s = this;
    }

    [SerializeField] private AudioClip m_honk;
    public AudioClip honk { get { return m_honk; } }

    [SerializeField] private AudioClip m_pass;
    public AudioClip pass { get { return m_pass; } }

    public static CarAudio get() { return s; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnmowerAudio : MonoBehaviour
{
    private static LawnmowerAudio s;

    void Awake()
    {
        if (s != null)
        {
            Debug.LogError("Attempted to instantiate multiple singletons");
        }

        s = this;
    }

    [SerializeField] private AudioClip m_startup;
    public AudioClip startup { get { return m_startup; } }
    [SerializeField] private AudioClip m_running;
    public AudioClip running { get { return m_running; } }
    public static LawnmowerAudio get() { return s; }
}

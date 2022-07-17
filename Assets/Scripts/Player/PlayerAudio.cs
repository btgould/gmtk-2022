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

    [SerializeField] private AudioClip m_shake;
    public AudioClip shake { get { return m_shake; } }

    public PlayerAudio get() { return s; }
}

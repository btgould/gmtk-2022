using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCCar : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private int duration;

    private Rigidbody2D rb;
    private bool triggered = false;
    private bool driving = false;
    private float driveStartTime;

    private AudioSource source;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!triggered) return;
        if (!driving)
        {
            driveStartTime = Time.time;
            driving = true;
            source.Play();
        }

        if (Time.time - driveStartTime < duration)
        {
            rb.AddForce(speed * rb.mass * direction * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    public void go()
    {
        triggered = true;
    }
}

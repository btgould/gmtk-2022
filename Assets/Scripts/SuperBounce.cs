using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBounce : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float mag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set velocity to be parallel to direction
        Vector2 vel = other.attachedRigidbody.velocity;
        vel = Vector2.Dot(vel, direction) * direction / direction.sqrMagnitude;
        other.attachedRigidbody.velocity = vel;

        // Apply impulse in direction
        other.attachedRigidbody.AddForce(mag * direction, ForceMode2D.Impulse);
    }
}

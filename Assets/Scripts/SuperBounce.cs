using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBounce : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float mag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.attachedRigidbody.AddForce(mag * direction, ForceMode2D.Impulse);
    }
}

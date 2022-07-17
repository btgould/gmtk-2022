using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControl : MonoBehaviour
{
    [SerializeField] private RCCar car;
    private bool triggered = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (!triggered)
            {
                car.go();
                triggered = true;
            }
        }
    }
}

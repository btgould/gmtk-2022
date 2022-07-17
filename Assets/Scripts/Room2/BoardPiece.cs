using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float strength;

    private Rigidbody2D rb;
    private bool hopping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void hop(int times, float delay = 1.2f)
    {
        if (hopping) return;
        hopping = true;

        for (int i = 0; i < times; i++)
        {
            Invoke("singleHop", i * delay);
        }

        Invoke("stopHop", times * delay);
    }

    private void singleHop()
    {
        rb.AddForce(strength * rb.mass * direction, ForceMode2D.Impulse);
    }

    private void stopHop() { hopping = false; }
}

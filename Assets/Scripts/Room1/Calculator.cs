using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    [SerializeField] private string fullNums = "43770";
    [SerializeField] private int delay = 15;

    private int dispIdx = 0;
    private int currDelay = 0;

    private Text numbers;

    void Start()
    {
        numbers = GetComponentInChildren<Text>();
        numbers.text = "";
    }

    void FixedUpdate()
    {
        currDelay--;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") && currDelay <= 0)
        {
            currDelay = delay;

            if (dispIdx < fullNums.Length)
            {
                dispIdx++;
                numbers.text = fullNums.Substring(0, dispIdx);
            }
        }
    }
}

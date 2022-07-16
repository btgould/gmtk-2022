using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    [SerializeField] private string fullNums = "43770";
    private int dispIdx = 0;

    private Text numbers;

    void Start()
    {
        numbers = GetComponentInChildren<Text>();
        numbers.text = "";
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            if (dispIdx < fullNums.Length)
            {
                dispIdx++;
                numbers.text = fullNums.Substring(0, dispIdx);
            }
        }
    }
}

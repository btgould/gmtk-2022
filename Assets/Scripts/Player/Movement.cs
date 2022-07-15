using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float jumpMag = 10;

    [SerializeField] private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(jumpMag * Vector2.up, ForceMode2D.Impulse);
        }
    }
}

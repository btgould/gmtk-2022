using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float startingZ = 0.1f;
    [SerializeField] private float solidZ = 2;
    [SerializeField] private float endingZ = 3;

    private SpriteRenderer sprite;
    private Vector3 startingScale;
    private BoxCollider2D boxCollider;
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startingScale = sprite.transform.localScale;
        z = startingZ;
        sprite.transform.localScale = z * startingScale;

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    void FixedUpdate()
    {
        z += speed * Time.deltaTime;
        sprite.transform.localScale = z * startingScale;

        if (z > endingZ) Destroy(gameObject);
        else if (z >= solidZ) boxCollider.enabled = true;
    }
}

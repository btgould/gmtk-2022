using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float duration = 3;
    [SerializeField] private float solidTime = 2;
    [SerializeField] private float startingZ = 0.1f;
    [SerializeField] private float endingZ = 3;
    [SerializeField] private float minY = 34;
    [SerializeField] private float maxY = -34;


    private SpriteRenderer sprite;
    private Vector3 startingScale;
    private BoxCollider2D boxCollider;
    private float z;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        startingScale = sprite.transform.localScale;
        z = startingZ;
        sprite.transform.localScale = z * startingScale;

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        startTime = Time.time;
    }

    void FixedUpdate()
    {
        float elapsed = Time.time - startTime;
        float t = elapsed / duration;
        z = Mathf.Lerp(startingZ, endingZ, t);
        sprite.transform.localScale = z * startingScale;
        float newY = Mathf.Lerp(minY, maxY, t);
        Vector3 pos = sprite.transform.position;
        pos.y = newY;
        sprite.transform.position = pos;

        if (t > 1) Destroy(gameObject);
        else if (elapsed > solidTime) boxCollider.enabled = true;
    }
}

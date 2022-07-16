using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lawnmower : MonoBehaviour
{
    private enum LawnmowerState { HIDDEN, STARTUP, RUNNING, BROKEN };
    private LawnmowerState state = LawnmowerState.HIDDEN;

    [SerializeField] private GameObject player;
    [SerializeField] private int startupTime = 60;
    [SerializeField] private GameObject mowedMask;
    private float scaleToTransRatio = 3f / 4;

    private Life playerLife;
    private PathFollow pathFollow;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collision;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = player.GetComponent<Life>();
        pathFollow = GetComponent<PathFollow>();
        pathFollow.enabled = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();
        appear(false);
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case LawnmowerState.HIDDEN:
                break;
            case LawnmowerState.STARTUP:
                if (startupTime <= 0)
                {
                    pathFollow.enabled = true;
                    state = LawnmowerState.RUNNING;
                }
                startupTime--;
                break;
            case LawnmowerState.RUNNING:
                float scale = mowedMask.transform.position.x - transform.position.x;
                Vector3 mowedScale = mowedMask.transform.localScale;
                mowedScale.x = scale * scaleToTransRatio;
                mowedMask.transform.localScale = mowedScale;

                if (pathFollow.isPathFinished())
                {
                    state = LawnmowerState.BROKEN;
                    pathFollow.enabled = false;
                }
                break;
            case LawnmowerState.BROKEN:
                appear(false);
                break;
            default:
                Debug.LogError("Invalid Lawnmower state");
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            playerLife.living = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            state = LawnmowerState.STARTUP;
            appear(true);
        }
    }

    private void appear(bool appear)
    {
        spriteRenderer.enabled = appear;
        collision.enabled = appear;
    }
}

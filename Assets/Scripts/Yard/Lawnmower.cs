using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lawnmower : MonoBehaviour
{
    private enum LawnmowerState { STARTUP, RUNNING, BROKEN };
    private LawnmowerState state = LawnmowerState.STARTUP;

    [SerializeField] private GameObject player;
    [SerializeField] private int startupTime = 60;

    private Life playerLife;
    private PathFollow pathFollow;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = player.GetComponent<Life>();
        pathFollow = GetComponent<PathFollow>();
        pathFollow.enabled = false;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case LawnmowerState.STARTUP:
                if (startupTime <= 0)
                {
                    pathFollow.enabled = true;
                    state = LawnmowerState.RUNNING;
                }
                startupTime--;
                break;
            case LawnmowerState.RUNNING:
                if (pathFollow.isPathFinished())
                {
                    state = LawnmowerState.BROKEN;
                    pathFollow.enabled = false;
                }
                break;
            case LawnmowerState.BROKEN:
                // TODO: something to show brokeness / disappear / etc.
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DS : MonoBehaviour
{
    private bool playing = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") && !playing)
        {
            playing = true;
            playGamePreview();
        }
    }

    private void playGamePreview()
    {
        Debug.Log("Play cutscene here");
        // TODO: add cutscene
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;

    private bool started = false;
    private bool ended = false;

    // Update is called once per frame
    void Update()
    {
        if (player.isPlaying) started = true;
        if (started && !player.isPlaying) ended = true;

        if (ended) title();
    }

    private void title()
    {
        SceneManager.LoadScene("Title");
    }
}

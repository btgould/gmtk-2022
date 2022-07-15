using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject endZone;
    private EndZone ez;

    void Awake()
    {
        ez = endZone.GetComponent<EndZone>();
    }

    void Update()
    {
        if (ez.beenEntered())
        {
            endScene();
        }
    }

    private void endScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            // Go to next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Quit out for now
            Application.Quit();
        }
    }
}

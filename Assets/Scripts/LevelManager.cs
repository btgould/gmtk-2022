using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject endZone;
    private EndZone ez;

    private GameObject player;
    private Life playerLife;
    private Vector3 playerStartPoint;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerStartPoint = player.transform.position;
        playerLife = player.GetComponent<Life>();
        ez = endZone.GetComponent<EndZone>();
    }

    void Update()
    {
        if (ez.beenEntered())
        {
            endScene();
        }

        if (!playerLife.living)
        {
            restart();
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

    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // if (player != null) Destroy(player);

        // player = Instantiate(playerPrefab, playerStartPoint, Quaternion.identity);
        // ICinemachineCamera vcam = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera;
        // vcam.Follow = player.transform;
    }
}

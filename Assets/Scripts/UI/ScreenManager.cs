using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private string credits = "Title1"; // Don't ask...

    public void PlayGame()
    {
        SceneManager.LoadScene("Room1");
    }

    public void Credits()
    {
        SceneManager.LoadScene(credits);
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

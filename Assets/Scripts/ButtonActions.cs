using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void LoadSettings()
    {
        GameManager.Instance.LoadScene("Settings");
    }
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void LoadLeaderboard()
    {
        GameManager.Instance.LoadScene("Leaderboard");
    }


    
}

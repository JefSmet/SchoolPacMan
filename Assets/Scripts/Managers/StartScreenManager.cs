using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
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
}

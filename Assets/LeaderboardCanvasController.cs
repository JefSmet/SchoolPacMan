using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject newHighScore;
    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private TextMeshProUGUI playerName;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (GameManager.Instance.IsScoreInTop5(GameManager.Instance.GameScore))
        {
            newHighScore.SetActive(true);            
        }
        else
        {
            defeatCanvas.SetActive(true);
        }
    }

    public void SaveHighscore()
    {
        GameManager.Instance.AddAndSort(new PlayerScore(playerName.text, GameManager.Instance.GameScore));
        GameManager.Instance.SaveScores();
        GameManager.Instance.LoadScene("Leaderboard");
    }
}

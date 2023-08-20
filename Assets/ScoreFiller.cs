using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreFiller : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> scoreTexts = new List<TextMeshProUGUI>();
    private void Start()
    {
        int counter=Mathf.Min(4,GameManager.Instance.playerScores.scores.Count-1);
        for (int i = 0; i <= counter; i++)
        {
            scoreTexts[i].text=i+1+". "+GameManager.Instance.playerScores.scores[i].playerName+": "+ GameManager.Instance.playerScores.scores[i].score;
        }
    }
}

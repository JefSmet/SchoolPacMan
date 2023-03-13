using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using QuestMan.Observer;

public class HUDController : MonoBehaviour 
{
    
    [SerializeField] TextMeshProUGUI _spText;
    [SerializeField] TextMeshProUGUI _ballsToGoText;
    [SerializeField] TextMeshProUGUI _playerSpeedText;    

    string _captionSP = "Studiepunten: ";
    string _captionBallsToGo = "StudieBallen te gaan: ";
    string _captionPlayerSpeed = "Loopsnelheid: ";

    public void SetSPText(int studiepunten)
    {
        _spText.text = _captionSP + studiepunten;
    }

    public void SetBallsToGoText(int ballsToGo)
    {
        _ballsToGoText.text = _captionBallsToGo + ballsToGo;
    }

    public void SetPlayerSpeedText(float playerspeed)
    {
        _playerSpeedText.text = _captionPlayerSpeed + string.Format("{0,5:##0.0}", playerspeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSPText(0);
        SetBallsToGoText(LevelManager.Instance.Studiepunten.Count);
    }
}

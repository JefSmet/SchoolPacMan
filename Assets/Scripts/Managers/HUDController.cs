using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour 
{
    
    [SerializeField] TextMeshProUGUI _spText;
    [SerializeField] TextMeshProUGUI _ballsToGoText;
    [SerializeField] TextMeshProUGUI _playerSpeedText;

    [SerializeField] string captionSP = "Studiepunten: ";
    [SerializeField] string captionBallsToGo = "StudieBallen te gaan: ";
    [SerializeField] string captionPlayerSpeed = "Loopsnelheid: ";

    public void SetSPText(int studiepunten)
    {
        _spText.text = captionSP + studiepunten;
    }

    public void SetBallsToGoText(int ballsToGo)
    {
        _ballsToGoText.text = captionBallsToGo + ballsToGo;
    }

    public void SetPlayerSpeedText(float playerspeed)
    {
        _playerSpeedText.text = captionPlayerSpeed + string.Format("{0,5:##0.0}", playerspeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSPText(0);
        SetBallsToGoText(LevelManager.Instance.Studiepunten.Count);
    }

    void PotentioChanged(int value)
    {
        SetPlayerSpeedText(value);
    }
}

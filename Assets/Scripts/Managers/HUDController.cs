using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class HUDController : MonoBehaviour 
{
    
    [SerializeField] private TextMeshProUGUI _spText;
    [SerializeField] private TextMeshProUGUI _ballsToGoText;
    [SerializeField] private TextMeshProUGUI _playerSpeedText;
    [SerializeField] private TextMeshProUGUI _livesText;

    [SerializeField] private string captionSP = "Studiepunten: ";
    [SerializeField] private string captionBallsToGo = "StudieBallen te gaan: ";
    [SerializeField] private string captionPlayerSpeed = "Loopsnelheid: ";
    [SerializeField] private string captionLives = "Levens: ";

    private FirstPersonController _firstPersonController;

    private void OnEnable()
    {
        
    }
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

    public void SetLivesText(int lives)
    {
        _livesText.text = captionLives + lives;
    }

}

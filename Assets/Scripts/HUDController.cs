using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Observer;
using TMPro;

public class HUDController : MonoBehaviour 
{
    
    [SerializeField] TextMeshProUGUI _studiepuntenText;
    [SerializeField] TextMeshProUGUI _studiepuntenToGoText;
    [SerializeField] TextMeshProUGUI potValueText;


    // Start is called before the first frame update
    void Start()
    {
       
        
        _studiepuntenText.text = "Studiepunten: 0";
        _studiepuntenToGoText.text = "Overblijvende Studiepunten: "+LevelManager.Instance.StudiepuntenValues.ToString();
    }

    public void UpdateStudiepunten(int currentStudiepunten)
    {
        _studiepuntenText.text = "Studiepunten: " + currentStudiepunten;
        _studiepuntenToGoText.text = "Overblijvende Studiepunten: " + LevelManager.Instance.StudiepuntenValues.ToString();
    }

    private void Update()
    {
        potValueText.text= "Potvalue: "+GameManager.Instance.ArduinoController.PotValue.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Observer;
using TMPro;

public class HUDController : Observer
{
    ScoreController _scoreController;
    TextMeshProUGUI _textMeshProUGUI;
    public override void Notify(Subject subject)
    {
        if (!_scoreController)
            _scoreController =
            subject.GetComponent<ScoreController>();
        if (_scoreController)
        {
            _textMeshProUGUI.text = "Studiepunten: " + _scoreController.studiepunten;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _textMeshProUGUI=this.GetComponentInChildren<TextMeshProUGUI>();
        _textMeshProUGUI.text = "Studiepunten: 0";
    }
}

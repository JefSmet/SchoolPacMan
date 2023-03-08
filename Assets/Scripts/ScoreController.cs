using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Observer;

public class ScoreController : Subject
{
    public int studiepunten; 


    
    HUDController _hudController;

    private void Awake()
    {
        _hudController=GameObject.FindObjectOfType<HUDController>();
    }

    private void OnEnable()
    {
        if (_hudController != null)
        {
            Attach(_hudController);
        }
    }

    private void OnDisable()
    {
        if (_hudController != null)
        {
            Detach(_hudController);
        }
    }

    public void AddStudiepunten(int aantal)
    {
        studiepunten += aantal;
        if(studiepunten < 0)
        {
            studiepunten = 0;
        }
        NotifyObservers();
    }

}

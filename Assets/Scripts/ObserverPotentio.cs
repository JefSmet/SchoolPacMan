using QuestMan.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverPotentio : Observer<int>
{
    public override void OnSubjectChanged(int value)
    {
        int speed = value.Remap(0, 1023, 1, 10);
        GameManager.Instance.HudController.SetPlayerSpeedText(speed);
    }
}

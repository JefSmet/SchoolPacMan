using QuestMan.Observer;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Player
{
    public class Questman : Observer_Int    
    {
        [SerializeField] FirstPersonController controller;
        [SerializeField] int heatlh;

        public override void OnIntChanged(int value)
        {
            float ms = value;
            ms = ms.Remap(0, 1023, 1, 10);
            controller.MoveSpeed = ms;
            controller.SprintSpeed = ms * 1.5f;
            GameManager.Instance.HudController.SetPlayerSpeedText(ms);
        }
    }
}


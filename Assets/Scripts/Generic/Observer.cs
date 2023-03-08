// adapted from packtpub "GAME_DEVELOPMENT_PATTERNS_WITH_UNITY_2021"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Observer
{     
    public abstract class Observer : MonoBehaviour
    {
        public abstract void Notify(Subject subject);
    }

}

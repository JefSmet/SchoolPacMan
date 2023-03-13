// adapted from unity e-book p. 79-87
// see: https://resources.unity.com/games/level-up-your-code-with-game-programming-patterns

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Observer
{
    public abstract class Observer_Int : MonoBehaviour
    {
        [SerializeField]
        Subject_Int subjectintToObserve;
        public abstract void OnIntChanged(int value);
        //{
        //    // any logic that responds to event goes here
        //    Debug.Log("Observer_Int responded. value = "+value);
        //}
        void Awake()
        {
            if (subjectintToObserve != null)
            {
                subjectintToObserve.IntChanged += OnIntChanged;
            }
        }
        void OnDestroy()
        {
            if (subjectintToObserve != null)
            {
                subjectintToObserve.IntChanged -= OnIntChanged;
            }
        }
    }
}

// adapted from unity e-book p. 79-87
// see: https://resources.unity.com/games/level-up-your-code-with-game-programming-patterns

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Observer
{
    public abstract class Observer : MonoBehaviour
    {
        [SerializeField] Subject subjectToObserve;
        public abstract void OnSubjectChanged();

        void Awake()
        {
            if (subjectToObserve != null)
            {
                subjectToObserve.SubjectChanged += OnSubjectChanged;
            }
        }
        void OnDestroy()
        {
            if (subjectToObserve != null)
            {
                subjectToObserve.SubjectChanged -= OnSubjectChanged;
            }
        }
    };

    public abstract class Observer<T> : MonoBehaviour 
    {
        [SerializeField] Subject<T> subjectToObserve;
        public abstract void OnSubjectChanged(T value);

        void Awake()
        {
            if (subjectToObserve != null)
            {
                subjectToObserve.SubjectChanged += OnSubjectChanged;
            }
        }
        void OnDestroy()
        {
            if (subjectToObserve != null)
            {
                subjectToObserve.SubjectChanged -= OnSubjectChanged;
            }
        }
    };
}
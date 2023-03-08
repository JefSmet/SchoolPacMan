// adapted from packtpub "GAME_DEVELOPMENT_PATTERNS_WITH_UNITY_2021"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Observer
{
    public abstract class Subject : MonoBehaviour
    {
        private readonly List<Observer> _observers = new List<Observer>();
        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }
        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }
        public void NotifyObservers()
        {
            foreach (Observer observer in _observers)
            {
                observer.Notify(this);
            }
        }
    }

}
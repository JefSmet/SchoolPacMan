// adapted from unity e-book p. 79-87
// see: https://resources.unity.com/games/level-up-your-code-with-game-programming-patterns

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Observer
{
    public class Subject : MonoBehaviour
    {
        public event Action SubjectChanged;

        public void NotifyObservers()
        {
            // "?.": Null-conditional operator
            // see https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-

            SubjectChanged?.Invoke();
        }
    };

    public class Subject<T> : MonoBehaviour 
    {
        public event Action<T> SubjectChanged;

        public void NotifyObservers(T value)
        {
            // "?.": Null-conditional operator
            // see https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-

            SubjectChanged?.Invoke(value);
        }
    };
}
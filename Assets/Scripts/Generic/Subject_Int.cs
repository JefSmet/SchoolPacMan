using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject_Int : MonoBehaviour
{
    public event Action<int> IntChanged;
    public void NotifyObservers(int value)
    {
        // "?.": Null-conditional operator
        // see https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-
        
        IntChanged?.Invoke(value);
    }
}

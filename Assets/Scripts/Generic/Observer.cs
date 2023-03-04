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

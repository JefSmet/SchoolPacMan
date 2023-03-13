using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Singleton
{
    public class Singleton<T> : MonoBehaviour
        where T : Component
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }

    // adapted from unity e-book p. 53-60
    // see: https://resources.unity.com/games/level-up-your-code-with-game-programming-patterns
    public class SingletonPersistent<T> : MonoBehaviour
        where T : Component
    {
        static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        SetupInstance();
                    }
                }
                return _instance;
            }
        }
        public virtual void Awake()
        {
            RemoveDuplicates();
        }
        static void SetupInstance() 
        {
            // FTK: the getter above already checked the following, so commented it out... 
            //_instance = (T)FindObjectOfType(typeof(T));
            //if (_instance == null)
            //{
                GameObject gameObj = new GameObject();
                gameObj.name = typeof(T).Name;
                _instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            //}
        }
        void RemoveDuplicates()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestMan.Singleton
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : Component
    {
        private static T _instance;
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
    public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
        where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        SetupInstance();
                    }
                }
                return instance;
            }
        }
        public virtual void Awake()
        {
            RemoveDuplicates();
        }
        private static void SetupInstance()
        {
            instance = (T)FindObjectOfType(typeof(T));
            if (instance == null)
            {
                GameObject gameObj = new GameObject();
                gameObj.name = typeof(T).Name;
                instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            }
        }
        private void RemoveDuplicates()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        //public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
        //    where T : Component
        //{
        //    public static T Instance { get; private set; }

        //    public virtual void Awake()
        //    {
        //        if (Instance == null)
        //        {
        //            Instance = this as T;
        //            DontDestroyOnLoad(this);
        //        }
        //        else
        //        {
        //            Destroy(gameObject);
        //        }
        //    }
        //}
    }
}
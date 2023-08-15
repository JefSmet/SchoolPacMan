using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Studiepunt : MonoBehaviour
{
    [SerializeField] private int _value = 5;

    public int Value
    {
        get { return _value; }
        set => _value = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.LevelManager.StudiepuntPickedUp(this);
        }        
    }

    


}

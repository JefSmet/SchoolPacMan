using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Studiepunt : MonoBehaviour
{
    [SerializeField] int value;

    public int Value { get { return value; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //GameManager.Instance.AddStudiepunt(this);
            //GameManager.Instance.ArduinoController.Blink();
            
        }        
    }

    


}

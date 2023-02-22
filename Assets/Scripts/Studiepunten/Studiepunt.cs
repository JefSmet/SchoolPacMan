using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Studiepunt : MonoBehaviour
{
    [SerializeField] int value;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.AddScore(value);
        }
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkScript : MonoBehaviour
{
    public OffMeshLink link;
    public MeshRenderer[] array;
    
    // Start is called before the first frame update
    void Start()
    {
        link= GetComponent<OffMeshLink>();

        array=GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer go in array)
        {
            go.enabled = false;
        }
    }

   
}

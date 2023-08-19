using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    [SerializeField] private GameObject leftEye;
    [SerializeField] private GameObject rightEye;
    private Renderer leftEyeRenderer;
    private Renderer rightEyeRenderer;


    private void Start()
    {
        leftEyeRenderer = leftEye.GetComponent<Renderer>();
        rightEyeRenderer = rightEye.GetComponent<Renderer>();
    }

    public void SwitchEyeColor(Color color)
    {
        leftEyeRenderer.material.color = color;
        rightEyeRenderer.material.color = color;
    }
}

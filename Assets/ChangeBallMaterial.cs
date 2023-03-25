using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeBallMaterial : MonoBehaviour
{
    Material material;
    MeshRenderer meshRenderer;
    Color ballColor;
    XRGrabInteractable interactable;
    float ballVelocity;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer= GetComponent<MeshRenderer>();
        material= meshRenderer.material;
        ballColor= material.color;
        interactable= GetComponent<XRGrabInteractable>();
        ballVelocity = interactable.throwVelocityScale;
    }

    public void ToggleColor()
    {
        if (material.color==ballColor)
        {
            material.color = Color.red;
            interactable.throwVelocityScale = ballVelocity*2;
        }
        else
        {
            material.color = ballColor;
            interactable.throwVelocityScale = ballVelocity;
        }
    }
}

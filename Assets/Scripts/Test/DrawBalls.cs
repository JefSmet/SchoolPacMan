using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBalls : MonoBehaviour
{
    public Transform startBall;
    public Transform endBall;
    public GameObject ball;
    public int ballCount;
    public void DoDrawBalls()
    {
        int remainder = (ballCount - 1);
        float perc = 1.0f / remainder;
        float curr = perc;
        //GameObject.Instantiate(ball, startBall.position, Quaternion.identity);
        //GameObject.Instantiate(ball, endBall.position, Quaternion.identity);
        while (curr < 1)
        {
            GameObject.Instantiate(ball, Vector3.Lerp(startBall.position, endBall.position, curr), Quaternion.identity);
            curr += perc;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

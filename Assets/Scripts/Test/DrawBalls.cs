using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBalls : MonoBehaviour
{
    public GameObject startBall;
    public GameObject endBall;
    public GameObject ball;
    public float colliderRadius = 2f;
    [MinAttribute(2)]
    public int ballCount=2;    

    private List<Vector3> CalcPositions()
    {
        List<Vector3> ballPositions = new List<Vector3>();        
        if (ballCount > 2)
        {
            float perc = 1.0f / (ballCount - 1);
            float currPerc = perc;
            while (currPerc < 1)
            {
                Vector3 pos = Vector3.Lerp(startBall.transform.position, endBall.transform.position, currPerc);
                ballPositions.Add(pos);                
                currPerc += perc;
            }
        }
        return ballPositions;
    }
    public void DoDrawBalls()
    {
        List<Vector3> ballPositions = CalcPositions();
        foreach (Vector3 pos in ballPositions)
        {
            GameObject studiepunt = Instantiate(ball,pos,Quaternion.identity);
            SphereCollider collider = studiepunt.GetComponent<SphereCollider>();
            collider.radius = colliderRadius;
        }
        startBall.GetComponent<SphereCollider>().radius= colliderRadius;
        endBall.GetComponent<SphereCollider>().radius = colliderRadius;
    }

    private void OnDrawGizmos()
    {
        if (startBall && endBall)
        {
            
            Renderer rend = startBall.GetComponent<Renderer>();
            
            Gizmos.color = rend.sharedMaterial.color;
            Gizmos.DrawLine(startBall.transform.position, endBall.transform.position);
            if (ballCount > 2)
            {
                List<Vector3> ballPositions = CalcPositions();
                SphereCollider collider = startBall.GetComponent<SphereCollider>();
                float radius = collider.radius;
                foreach (Vector3 pos in ballPositions)
                {
                    Gizmos.DrawWireSphere(pos, radius);
                }               
            }                  
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DoDrawBalls();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DrawBalls : MonoBehaviour
{
    
    public GameObject startBall;
    public GameObject endBall;
    public GameObject ballPrefab;
    public float colliderRadius = 2f;

    [MinAttribute(2)] public int ballCount=2;
    [SerializeField] bool showCollidergizmo = false;

    private List<Vector3> GetBallPositions()
    {
        List<Vector3> ballPositions = new List<Vector3>();        
        if (ballCount > 2)  // Only if more then startBall and endBall need to be drawn
        {
            float percentage = 1.0f / (ballCount - 1);
            float currPercentage = percentage;
            while (currPercentage < 1)
            {
                Vector3 pos = Vector3.Lerp(startBall.transform.position, endBall.transform.position, currPercentage);
                ballPositions.Add(pos);                
                currPercentage += percentage;
            }
        }
        return ballPositions;
    }
    public void DoDrawBalls()
    {
        List<Vector3> ballPositions = GetBallPositions();
        foreach (Vector3 position in ballPositions)
        {
            GameObject studiepunt = Instantiate(ballPrefab, position, Quaternion.identity);
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
                List<Vector3> ballPositions = GetBallPositions();
                // 
                // How to calculate the radius of a sphere?
                //
                // The mesh of a component contains an array of vertices (positions) in local space.
                // In a sphere, all vertices have an equal distance (radius) to the center (at Vector3.zero in local space).
                // Any vertex of the vertices-array is OK to calculate the radius, so just take the first.
                // 
                Mesh mesh = startBall.GetComponent<MeshFilter>().sharedMesh;
                Vector3 vertex = mesh.vertices.First<Vector3>();
                float radius = Vector3.Distance(Vector3.zero, vertex);                
                foreach (Vector3 pos in ballPositions)
                {
                    Gizmos.DrawWireSphere(pos, radius);
                    if (showCollidergizmo)
                    {
                        Gizmos.DrawWireSphere(pos, colliderRadius);
                    }
                }               
            }
            if (showCollidergizmo)
            {
                Gizmos.DrawWireSphere(startBall.transform.position, colliderRadius);
                Gizmos.DrawWireSphere(endBall.transform.position, colliderRadius);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DoDrawBalls();
    }
}

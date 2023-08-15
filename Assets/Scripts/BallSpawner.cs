using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BallSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject _startBall;
    [SerializeField] private GameObject _endBall;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _colliderRadius = 2f;
    [SerializeField] [MinAttribute(2)] private int _ballCount=2;
    [SerializeField] private bool _showCollidergizmo = false;

    void Awake()
    {
        InstantiateBalls();
        _startBall.GetComponent<SphereCollider>().radius = _colliderRadius;
        _endBall.GetComponent<SphereCollider>().radius = _colliderRadius;
    }

    void OnDrawGizmos()
    {
        if (_startBall != null && _endBall != null)
        {
            Renderer rend = _startBall.GetComponent<Renderer>();

            Gizmos.color = rend.sharedMaterial.color;
            Gizmos.DrawLine(_startBall.transform.position, _endBall.transform.position);

            if (_ballCount > 2)
            {
                List<Vector3> ballPositions = GetBallPositions();
                float radius = GetBallRadius(_startBall);
                foreach (Vector3 pos in ballPositions)
                {
                    Gizmos.DrawWireSphere(pos, radius);
                    if (_showCollidergizmo)
                    {
                        Gizmos.DrawWireSphere(pos, _colliderRadius);
                    }
                }
            }
            if (_showCollidergizmo)
            {
                Gizmos.DrawWireSphere(_startBall.transform.position, _colliderRadius);
                Gizmos.DrawWireSphere(_endBall.transform.position, _colliderRadius);
            }
        }
    }

    float GetBallRadius(GameObject sphere)
    {
        // 
        // How to calculate the radius of a sphere?
        //
        // The mesh of a component contains an array of vertices (positions) in local space.
        // In a sphere, all vertices have an equal distance (radius) to the center (at Vector3.zero in local space).
        // Any vertex of the vertices-array is OK to calculate the radius, so just take the first.
        // 
        Mesh mesh = _startBall.GetComponent<MeshFilter>().sharedMesh;
        Vector3 vertex = mesh.vertices.First<Vector3>();
        return Vector3.Distance(Vector3.zero, vertex);
    }

    List<Vector3> GetBallPositions()
    {
        List<Vector3> ballPositions = new List<Vector3>();
        if (_ballCount > 2)  // Only if more then startBall and endBall need to be drawn
        {
            float percentage = 1.0f / (_ballCount - 1);
            float currPercentage = percentage;
            while (currPercentage < 1)
            {
                Vector3 pos = Vector3.Lerp(_startBall.transform.position, _endBall.transform.position, currPercentage);
                ballPositions.Add(pos);
                currPercentage += percentage;
            }
        }
        return ballPositions;
    }

    void InstantiateBalls()
    {
        List<Vector3> ballPositions = GetBallPositions();
        foreach (Vector3 position in ballPositions)
        {
            GameObject studiepunt = Instantiate(_ballPrefab, position, Quaternion.identity);
            SphereCollider collider = studiepunt.GetComponent<SphereCollider>();
            collider.radius = _colliderRadius;
        }        
    }

   
}

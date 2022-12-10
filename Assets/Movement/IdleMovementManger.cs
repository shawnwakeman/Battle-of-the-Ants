using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovementManger : MonoBehaviour
{


    public EdgeCollider2D edgeCollider;


    public void GenerateCollider(Vector2 worldpos, int numEdges, float radius)
    {
        
        Vector2[] points = new Vector2[numEdges + 1];

        for (int i = 0; i < numEdges; i++)
        {
            float angle = 2 * Mathf.PI * i / numEdges;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);
            Debug.Log("fas");

        }

        points[^1] = points[0];
        edgeCollider.transform.position = new Vector3(0, 0, 0);
        edgeCollider.points = points;
        edgeCollider.offset = worldpos;
    }

    public void SetColliderWorldPos(Vector3 worldpos)
    {
        edgeCollider.offset = worldpos;
    }
}

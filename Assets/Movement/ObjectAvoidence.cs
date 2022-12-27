using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAvoidence : MonoBehaviour
{
    public Rigidbody2D agentRB;
    public int leftOrRight;

    public float viewAngle;
    private RaycastHit2D hit;
    private float RayOffset = 20;

    public Vector2 avoidenceVelocity;
    public bool avoidenceActivatated;
    void Start()
    {
        
    }

    // Update is called once per frame

    

    /// <returns>the offset velocity to avoid objects</returns>
    private void FixedUpdate() 
    {
        

        
        hit = Physics2D.Raycast(agentRB.position, agentRB.velocity, 5);
        Vector2 bruh = agentRB.velocity;
        if (hit.collider == null)
        {
            Debug.DrawRay(agentRB.position, agentRB.velocity.normalized * 5, Color.green);
            avoidenceVelocity = Vector2.Lerp(avoidenceVelocity, new Vector2(0, 0), 2 * Time.deltaTime);


        }
        else
        {

            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    hit = Physics2D.Raycast(agentRB.position, GetDirectionVector2D(agentRB.rotation + (RayOffset * i)), 5);
                    Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation + (RayOffset * i)) * 5, Color.red);
                    if (hit.collider == null) { avoidenceVelocity = GetDirectionVector2D(agentRB.rotation + RayOffset); }
                }
                else
                {
                    hit = Physics2D.Raycast(agentRB.position, GetDirectionVector2D(agentRB.rotation + (i * -RayOffset)), 5);
                    Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation + (-RayOffset * i)) * 5, Color.red);
                    if (hit.collider == null) { avoidenceVelocity = GetDirectionVector2D(agentRB.rotation + RayOffset); }
                }
            }

        }

        Debug.Log(avoidenceVelocity);
        agentRB.AddForce(avoidenceVelocity);


    }



    public Vector2 GetDirectionVector2D(float angle)
    {
         return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

}

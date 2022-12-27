using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agentavoidence : MonoBehaviour
{
    private RaycastHit2D middleCast;
    private RaycastHit2D leftCast;
    private RaycastHit2D rightCast;
    private RaycastHit2D leftCastExtended;
    private RaycastHit2D rightCastExtended;

    int layerMask;

    public Rigidbody2D agentRB;
    public float avoidenceforce;
    public float projectionDistance;
    public float maxspeed;
    public float innerangle;
    private Vector2 force;
    void Start()
    {
        layerMask = LayerMask.GetMask("unit");
    }
    // hit = Physics2D.Raycast(agentRB.position, GetDirectionVector2D(agentRB.rotation + (RayOffset * i)), 5);
    // Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation + (RayOffset * i)) * 5, Color.red);
    // if (hit.collider == null) { avoidenceVelocity = GetDirectionVector2D(agentRB.rotation + RayOffset); }
    // Update is called once per frame
    void FixedUpdate()
    {
        //might want to optimize
        // middleCast = Physics2D.Raycast(agentRB.position, GetDirectionVector2D(agentRB.rotation), projectionDistance);
        // Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation) * projectionDistance, Color.red);
        // if (middleCast.collider != null && middleCast.collider.gameObject.tag == "unit")
        // {
        //     agentRB.AddForce((Vector2.right * -1) * avoidenceforce);
        // }
        leftCast = Physics2D.Raycast(agentRB.position, GetDirectionVector2D(agentRB.rotation + innerangle), projectionDistance);
        Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation + innerangle) * projectionDistance, Color.red);
        if (leftCast.collider != null && leftCast.collider.gameObject.tag == "unit")
        {
            Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation + innerangle) * projectionDistance, Color.green);
            agentRB.velocity = (agentRB.velocity + GetDirectionVector2D(agentRB.rotation - (innerangle - 5)).normalized) * agentRB.velocity.magnitude;
        } 

        rightCast = Physics2D.Raycast(agentRB.position, GetDirectionVector2D(agentRB.rotation - innerangle), projectionDistance);
        Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation - innerangle) * projectionDistance, Color.red);
        if (rightCast.collider != null && rightCast.collider.gameObject.tag == "unit")
        {
            Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation - innerangle) * projectionDistance, Color.green);
            agentRB.velocity = (agentRB.velocity + GetDirectionVector2D(agentRB.rotation + (innerangle + 5)).normalized) * agentRB.velocity.magnitude;
        } 
        // Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation + 30) * projectionDistance, Color.red);
        // Debug.DrawRay(agentRB.position, GetDirectionVector2D(agentRB.rotation - 30) * projectionDistance, Color.red);
        // speedLimiter(agentRB);
    }

    public Vector2 GetDirectionVector2D(float angle)
    {
         return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void speedLimiter(Rigidbody2D agentRB)
    {
        if (agentRB.velocity.sqrMagnitude >= maxspeed*maxspeed)
        {
            agentRB.velocity = agentRB.velocity.normalized * maxspeed;
            
        }
        
    }
}

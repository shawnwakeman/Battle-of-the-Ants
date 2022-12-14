using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : MonoBehaviour
{

    public GridController gridController;
    public Rigidbody2D agentRB;
    public UnitState unitState;


    public float rotationAroundCircleSign;
    public float targetPosX;
    public float targetPosY;
    private float xVelocity;
    private float yVelocity;
    public float speed;
    public float rotation_speed;
    public float radRotation;
    public float lerp_speed;

    private Vector3 intended_pos;
    private float delta_rz;
    private float target_angle;
    private Vector3 destanation;
    private Vector2 ant;
    private Vector2 ant_destination;
    private float rotationVariation;
    private float radius;

    
    private float angle;

    private void Start() 
    {
        ant_destination = transform.position;
        rotationVariation = Random.Range(-.001f,.003f);
    }


    void Update()
    {

        if (unitState.currentStateInt == UnitState.uState.calculatingIdle)
        {
            angle = Mathf.Atan2(agentRB.position.y - gridController.worldPosition.y, agentRB.position.x - gridController.worldPosition.x);
            // Debug.Log(angle);
            // Debug.Log(angle * Mathf.Rad2Deg);
            // Debug.Log(Vector2.Distance(gridController.worldPosition, agentRB.position));
            // Debug.Log(agentRB.position);
            
            GetRotationDirection(agentRB.position, gridController.worldPosition);
            radius = Vector2.Distance(gridController.worldPosition, agentRB.position);
            if (radius < unitState.orbitLevel)
            {
                radius = unitState.orbitLevel;
            }
            unitState.currentStateInt = UnitState.uState.idle;


        }
        if (unitState.currentStateInt == UnitState.uState.idle)
        {
            // agentRB.isKinematic = true;
            MoveTowardsPoint();
        }
    }

    private void OnDrawGizmos() {

        Gizmos.DrawCube(new Vector3(targetPosX, targetPosY, 0), new Vector3(.1f, .1f, .1f));
        
    }
    private void MoveTowardsPoint()
    {

        agentRB.velocity *= .995f;
        targetPosX = (Mathf.Cos(angle) * radius) + gridController.worldPosition.x;
        targetPosY = (Mathf.Sin(angle) * radius) + gridController.worldPosition.y;
        Vector2 triangle_vectors = new((targetPosX - transform.position.x), (targetPosY - transform.position.y));
        xVelocity = triangle_vectors.x / speed;
        yVelocity = triangle_vectors.y / speed;

        

        ant = new(transform.position.x, transform.position.y);
        ant_destination = new(transform.position.x + xVelocity, transform.position.y + yVelocity);
        target_angle = AngleBetweenVector2(ant_destination, ant) + 90;

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                             Quaternion.Euler(0, 0, target_angle), rotation_speed * Time.deltaTime);

        
        // transform.rotation = Quaternion.Euler(0, 0, target_angle);
        // transform.rotation = Quaternion.Euler(0,0, display_angle);

        Vector2 vels = new(xVelocity, yVelocity);
        Vector2 pos = transform.position;

        

        transform.position = Vector2.Lerp(transform.position, ant_destination, speed * Time.deltaTime);
        
        

        angle += (radRotation * rotationAroundCircleSign) + rotationVariation;



    }

    private Vector2 getNextPosition()
    {
        return new Vector2(0,0);
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
         Vector2 diference = vec2 - vec1;
         float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
         
         return (Vector2.Angle(Vector2.right, diference) * sign) + 90;
     }

    public void GetRotationDirection(Vector2 pos, Vector2 centerOfCircle)
    { 
        if (pos.x < centerOfCircle.x && pos.y > centerOfCircle.y)
        {
            if (agentRB.velocity.x >= 0 && agentRB.velocity.y >=0)
            {
                //negitve
                rotationAroundCircleSign = -1;
            }
            else if(agentRB.velocity.x <= 0 && agentRB.velocity.y <= 0)
            {
                // positive
                rotationAroundCircleSign = 1;
            }
            else
            {
                //positive
                rotationAroundCircleSign = 1;
            }
        }
        else if (pos.x > centerOfCircle.x && pos.y > centerOfCircle.y)
        {
            if (agentRB.velocity.x <= 0 && agentRB.velocity.y <= 0)
            {
                // negitve
                rotationAroundCircleSign = -1;
            }
            else if(agentRB.velocity.x >= 0 && agentRB.velocity.y <= 0)
            {
                // negitive
                rotationAroundCircleSign = -1;
            }
            else
            {
                //positive
                rotationAroundCircleSign = 1;
            }
        }
        else if (pos.x < centerOfCircle.x && pos.y < centerOfCircle.y)
        {
            if(agentRB.velocity.x <= 0 && agentRB.velocity.y <= 0)
            {
                // negitve
                rotationAroundCircleSign = -1;
            }
            else if(agentRB.velocity.x >= 0 && agentRB.velocity.y >= 0)
            {
                //positive
                rotationAroundCircleSign = 1;
            }
            else
            {
                // negitve
                rotationAroundCircleSign = -1;
            }
        }
        else
        {
            if(agentRB.velocity.x <= 0 && agentRB.velocity.y <= 0)
            {
                // negitve
                rotationAroundCircleSign = -1;
            }
            else if(agentRB.velocity.x >= 0 && agentRB.velocity.y <= 0)
            {
                //positive
                rotationAroundCircleSign = 1;
            }
            else
            {
                // positive
                rotationAroundCircleSign = 1;
            }
        }
    }


}




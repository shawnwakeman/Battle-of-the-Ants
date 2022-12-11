using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : MonoBehaviour
{

    public UnitState unitState;
    private float x_vel;
    private float y_vel;
    public float speed;
    public float rotation_speed;
    
    public float lerp_speed;

    private Vector3 intended_pos;
    private float delta_rz;
    private float target_angle;
    private Vector3 destanation;
    private Vector2 ant;
    private Vector2 ant_destination;
 
    private void Start() 
    {
        ant_destination = transform.position;
    }


    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        if (unitState.currentStateInt == UnitState.uState.idle)
        {
            // code goes here
        }
        if (Input.GetMouseButton(0))
        {
            

            Vector2 triangle_vectors = new((worldPosition.x - transform.position.x) , (worldPosition.y - transform.position.y));
            x_vel = triangle_vectors.x / speed;
            y_vel = triangle_vectors.y / speed;



            ant = new(transform.position.x, transform.position.y);
            ant_destination = new(worldPosition.x, worldPosition.y);
            target_angle = AngleBetweenVector2(ant_destination, ant);
        
            


            
            

        }


        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, target_angle), rotation_speed * Time.deltaTime);
        // transform.rotation = Quaternion.Euler(0,0, display_angle);

        Vector2 vels = new(x_vel,y_vel);
        Vector2 pos = transform.position;
        

        if (Vector2.Distance(transform.position, ant_destination) < 1)
        {
            transform.position = Vector2.Lerp(transform.position , ant_destination, lerp_speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, ant_destination, speed * Time.deltaTime);
            
        }


        

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


    //     public void orbit(float radius)
    // {

    //     if (flowFieldSteer.setInitalAngle == false)
    //     {
    //         flowFieldSteer.angle = AngleBetweenVector20(agentRB.position, gridController.worldPosition);
    //         flowFieldSteer.angle = Mathf.Abs(flowFieldSteer.angle);
    //         flowFieldSteer.setInitalAngle = true;
            


    //     }
    //     else
    //     {
    //         float x = radius * Mathf.Cos(flowFieldSteer.angle) + gridController.worldPosition.x;
    //         float y = radius * Mathf.Sin(flowFieldSteer.angle) + gridController.worldPosition.y;

    //         flowFieldSteer.angle += -1 * Time.deltaTime;

    //         // Apply the force to the Rigidbody2D
    //         agentRB.MovePosition(new Vector2(x, y));
         
    //     }


    // }
}




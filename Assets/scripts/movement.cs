using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public MovementManager movementManager;
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
 
    void Start()
    {
        ant_destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
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

     private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
         Vector2 diference = vec2 - vec1;
         float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
         
         return (Vector2.Angle(Vector2.right, diference) * sign) + 90;
     }


}

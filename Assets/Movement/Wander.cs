using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    public void orbit(Rigidbody2D agentRB)
    {
        
        
        float radius = 2f;  // The radius of the circular path
        float speed = 5f;   // The speed at which the object moves along the path
        // Calculate the x and y components of the force to apply
        float xForce = Mathf.Sin(Time.time * speed) * radius;
        float yForce = Mathf.Cos(Time.time * speed) * radius;

        // Apply the force to the Rigidbody2D
        agentRB.AddForce(new Vector2(xForce, yForce));


    }
}

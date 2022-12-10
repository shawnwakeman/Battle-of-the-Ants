using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    
    public bool collided;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collided = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("fuck");
        collided = true;
        // GameObject other_ant = collision.gameObject;
        // if (other_ant.CompareTag("ant"))
        // {
        //    Debug.Log(other_ant.GetComponent<Transform>().position);
        // }
    }
}

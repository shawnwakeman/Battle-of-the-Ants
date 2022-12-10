using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : MonoBehaviour
{

    public bool setTrigger = false;

    public Vector2 edgeColliderPos;
    public PhysicsMaterial2D inIdleMovementMaterial;
    bool incollider;
    // public GridController gridController;

    void Update()
    {

        

    }
    


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "idlecollider")
        {
            GetComponent<Collider2D>().isTrigger = false;
            GetComponent<Collider2D>().sharedMaterial = inIdleMovementMaterial;
            GetComponent<Collider2D>().offset = edgeColliderPos;
            gameObject.layer = LayerMask.NameToLayer("unit");

        }

    }
}




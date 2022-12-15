using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPath : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D cursorRB;
    public float speed;
    public TrailRenderer trailRenderer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            trailRenderer.emitting = false;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            gameObject.transform.position = worldPosition;
        }
        else if (Input.GetMouseButton(0))
        {
            trailRenderer.emitting = true;
            trailRenderer.time = 5;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);  
            var pos = Input.mousePosition;
            pos.z = -Camera.main.transform.position.z;
            pos = Camera.main.ScreenToWorldPoint(pos);
            var dir = pos - transform.position;
            cursorRB.velocity = (dir * speed);
            speedLimiter(cursorRB);
            



        }
        else
        {
            cursorRB.velocity *= .99f;
            trailRenderer.emitting = false;
            trailRenderer.time = .5f;
        }
    }

    private void speedLimiter(Rigidbody2D agentRB)
    {
        if (agentRB.velocity.sqrMagnitude >= speed*speed)
        {
            agentRB.velocity = agentRB.velocity.normalized * speed;
            
        }
        
    }
}

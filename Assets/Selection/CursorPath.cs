using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CursorPath : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float pointCreationDistance;
    public SelectionState selectionState;
    public Rigidbody2D cursorRB;
    public TrailRenderer trailRenderer;
    public PolygonCollider2D polygonCollider2D;
    public float pointPollingRate;
    public List<Vector2> futurePoints = new List<Vector2>();
    Vector3 currentWorldPos;
    Vector2 startingPoint;
    Vector2 finishingPoint;
    Vector2 pointToBeAdded;


    private void Start() 
    {

        StartCoroutine(UpdatePointList());

    }
    void Update()
    {
        if(selectionState.currentState == SelectionState.sState.justStartedSelection)
        {
            futurePoints.Clear();
            SetStartTransfrom();
            startingPoint = currentWorldPos;
            trailRenderer.emitting = false;
            
        }
        else if (selectionState.currentState == SelectionState.sState.currentlySelecting)
        {
            trailRenderer.emitting = true;
            trailRenderer.time = 5;
            UpdateCursorMovement();
            pointToBeAdded = new Vector2(currentWorldPos.x, currentWorldPos.y);
            if (futurePoints.Count > 2000) // protect ageinst long statonaray impts
            {
                futurePoints.Clear();
            }
        }
        else if (selectionState.currentState == SelectionState.sState.finishedSelecting)
        {
            finishingPoint = currentWorldPos;
            if (futurePoints.Count > 1)
            {
                polygonCollider2D.points = futurePoints.ToArray();
            }
            trailRenderer.emitting = false;
            pointToBeAdded = Vector2.zero;
        }
        else // idling
        {
            cursorRB.velocity *= .99f;
            trailRenderer.emitting = false;
            trailRenderer.time = .5f;
        }
    }

    void speedLimiter(Rigidbody2D agentRB)
    {
        if (agentRB.velocity.sqrMagnitude >= speed*speed)
        {
            agentRB.velocity = agentRB.velocity.normalized * speed;
            
        }
        
    }


    IEnumerator UpdatePointList()
    {
        while (true)
        {
            if (pointToBeAdded != Vector2.zero)
            {
                if (futurePoints.Count > 0 && Vector2.Distance(pointToBeAdded, futurePoints[^1]) > pointCreationDistance)
                {
                    futurePoints.Add(new Vector2(currentWorldPos.x, currentWorldPos.y));
                }
                else if (futurePoints.Count == 0)
                {
                    futurePoints.Add(new Vector2(currentWorldPos.x, currentWorldPos.y));
                }
            }
            yield return new WaitForSeconds(pointPollingRate);
        }
    }


    void SetStartTransfrom()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.position = worldPosition;
    }

    void UpdateCursorMovement()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        currentWorldPos = Camera.main.ScreenToWorldPoint(mousePos);  
        var dir = currentWorldPos - transform.position;
        cursorRB.velocity = (dir * speed);
        speedLimiter(cursorRB);
    }


}   



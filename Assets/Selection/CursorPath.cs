using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CursorPath : MonoBehaviour
{
    // Start is called before the first frame update
    public SelectionState selectionState;
    public Rigidbody2D cursorRB;
    public float speed;
    public TrailRenderer trailRenderer;

    public List<Vector2> futurePoints = new List<Vector2>();

    public PolygonCollider2D edgeCollider2D;

    public float stopSelectingDistance;
    public GameObject transformprefab;
    // Update is called once per frame
    public List<GameObject> intemTransformlist = new List<GameObject>();
    Vector3 currentWorldPos;

    public Vector2 totalDistanceVector;

    public Vector2 startingPoint;
    public Vector2 finishingPoint;
    public Vector2 pointToBeAdded;


    public float pointCreationDistance;


    private void Start() 
    {

        StartCoroutine(UpdatePointList());

    }
    void Update()
    {
        if(selectionState.currentState == SelectionState.sState.justStartedSelection && disableImput == false)
        {
            totalDistanceVector = Vector2.zero;
            SetStartTransfrom();
            startingPoint = currentWorldPos;
            trailRenderer.emitting = false;
            
        }
        else if (selectionState.currentState == SelectionState.sState.currentlySelecting && disableImput == false)
        {
            trailRenderer.emitting = true;
            trailRenderer.time = 5;
            UpdateCursorMovement();
            pointToBeAdded = new Vector2(currentWorldPos.x, currentWorldPos.y);
            if (futurePoints.Count > 2000) // protect ageinst long statonaray impts
            {
                futurePoints.Clear();
            }
            Vector2 closestPoint = Vector2.zero;




        }
        else if (selectionState.currentState == SelectionState.sState.finishedSelecting && disableImput == false)
        {
            finishingPoint = currentWorldPos;
            if (futurePoints.Count > 0)
            {
                ConvertVector2ToTransfrom();
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

    void ConvertVector2ToTransfrom()
    {
        

        if (futurePoints.Count > 0)
        {
            foreach (Vector2 item in futurePoints)
            {
                GameObject transfromObject = Instantiate(transformprefab, new Vector3(item.x, item.y, 0), Quaternion.Euler(0, 0, 0));
                intemTransformlist.Add(transfromObject);
            }
            selectionState.SetTransforms(intemTransformlist);
            intemTransformlist.Clear();
            futurePoints.Clear();      
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
                    // Debug.Log(Vector2.Distance(pointToBeAdded, futurePoints[^1]));
                    futurePoints.Add(new Vector2(currentWorldPos.x, currentWorldPos.y));
                    edgeCollider2D.points = futurePoints.ToArray();


                }
                else if (futurePoints.Count == 0)
                {
                    // Debug.Log(Vector2.Distance(pointToBeAdded, futurePoints[^1]));
                    edgeCollider2D.points = futurePoints.ToArray();
                    futurePoints.Add(new Vector2(currentWorldPos.x, currentWorldPos.y));
                    // List<Vector2> colliderPoints = new List<Vector2>(edgeCollider2D.points); // needs to be optimized
                    // colliderPoints.Add(pointToBeAdded);
                    // edgeCollider2D.points =  colliderPoints.ToArray();

                }

            }
            yield return new WaitForSeconds(0.15f);

        }
    }

    VertexPath GeneratePath(Vector2[] points, bool closedPath)
    {
       // Create a closed, 2D bezier path from the supplied points array
       // These points are treated as anchors, which the path will pass through
       // The control points for the path will be generated automatically
       BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xy);
       // Then create a vertex path from the bezier path, to be used for movement etc
       return new VertexPath(bezierPath);
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
        totalDistanceVector += new Vector2(Mathf.Abs(cursorRB.velocity.x), Mathf.Abs(cursorRB.velocity.y));
        speedLimiter(cursorRB);
    }


}   



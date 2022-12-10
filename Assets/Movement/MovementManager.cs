using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();

    public GridController gridController;
    private float unitCounter = 1;
    public GameObject unitPrefab;

    FlowFieldSteer flowFieldSteer;
    ObjectAvoidence objectAvoidence;
    Rigidbody2D agentRB;
    Collider2D idleCircle;
    
    IdleMovement idleMovement;

    [Range(1, 10)]
    public float maxSpeed;

    void Start()
    {
        foreach ( Transform unit in transform)
        {
            if (unit.tag == "unit")
            {
                unitList.Add(unit.gameObject);
            }
        }

        foreach (GameObject unit in unitList)
        {
            Debug.Log(unit);
        }
    }

    void Update()
    {
        InstantiateNewUnit();
        if (unitList.Count == 0) { return; }
        foreach(GameObject unit in unitList)
        {
            flowFieldSteer = unit.GetComponent<FlowFieldSteer>();
            objectAvoidence = unit.GetComponent<ObjectAvoidence>();
            agentRB = unit.GetComponent<Rigidbody2D>();
            idleMovement = unit.GetComponent<IdleMovement>();
            if (gridController.destinationCell != null)
                {
                    if (Vector2.Distance(agentRB.position , gridController.destinationCell.worldPos) > 3)
                    {

                        flowFieldSteer.FlowFieldMovment();
                            
                    }
                    else
                    {
                        if (idleMovement.setTrigger != true)
                        {
                            idleMovement.setTrigger = true;
                            unit.GetComponent<Collider2D>().isTrigger = true;                            
                        }

                    }                
                }

            


            // objectAvoidence.GetAvoidenceVelocity();
            flowFieldSteer.RotateToVelocity();
            speedLimiter(agentRB);
        }
    }



    public Vector2 GetDirectionVector2D(float angle)
    {
         return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void speedLimiter(Rigidbody2D agentRb)
    {
        if (agentRB.velocity.sqrMagnitude >= maxSpeed*maxSpeed)
        {
            agentRB.velocity = agentRB.velocity.normalized * maxSpeed;
            
        }
    }
    public void InstantiateNewUnit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            GameObject newUnit = Instantiate(unitPrefab, worldPosition, Quaternion.Euler(0,0,0));
            newUnit.GetComponent<FlowFieldSteer>().gridController = gridController;
            unitCounter++;
            unitList.Add(newUnit);

        }
    }



}

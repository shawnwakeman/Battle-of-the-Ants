using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public GameObject gridControllerprefab;
    public SelectionState selectionState;
    public EdgeCollisionDetection edgeCollisionDetection;
    public GameObject unitPrefab;



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


        if (selectionState.currentState == SelectionState.sState.clickedToTarget)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 clickWorldPos = Camera.main.ScreenToWorldPoint(mousePos);


            GameObject newGridController = Instantiate(gridControllerprefab, transform.position, transform.rotation);
            newGridController.transform.parent = gameObject.transform;
            GridController gridObject = newGridController.GetComponent<GridController>();
            gridObject.worldPosition = clickWorldPos;
            foreach (Collider2D item in edgeCollisionDetection.overlappingColliders)
            {
                item.transform.parent = newGridController.transform;
                item.gameObject.GetComponent<IdleMovement>().gridController = gridObject;
                item.gameObject.GetComponent<TransitionMovement>().gridController = gridObject;
            }

            selectionState.currentState = SelectionState.sState.idle;

        }
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     foreach (GameObject unit in unitList)
        //     {
        //         unit.GetComponent<UnitState>().currentStateInt = UnitState.uState.transition;
        //     }
        // }
        InstantiateNewUnit();
    }



    public void InstantiateNewUnit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject newUnit = Instantiate(unitPrefab, worldPosition, Quaternion.Euler(0,0,0));
            // newUnit.GetComponent<IdleMovement>().gridController = gridController;
            // newUnit.GetComponent<TransitionMovement>().gridController = gridController;
            newUnit.transform.parent = gameObject.transform;
            unitList.Add(newUnit);

        }
    }
}


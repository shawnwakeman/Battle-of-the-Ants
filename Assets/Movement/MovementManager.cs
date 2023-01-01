using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] GameObject gridControllerPrefab;
    [SerializeField] SelectionState selectionState;
    [SerializeField] EdgeCollisionDetection edgeCollisionDetection;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] string unitID1;
    [SerializeField] int channel;

    public GameObject newGridController;
    public GridController gridObject;




    void Start()
    {
        
    }

    void Update()
    {

        if (selectionState.currentState == SelectionState.sState.clickedToTarget)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 clickWorldPos = Camera.main.ScreenToWorldPoint(mousePos);


            newGridController = Instantiate(gridControllerPrefab, transform.position, transform.rotation);
            newGridController.transform.parent = gameObject.transform;
            gridObject = newGridController.GetComponent<GridController>();
            gridObject.worldPosition = clickWorldPos;
            foreach (Collider2D item in edgeCollisionDetection.overlappingColliders)
            {
                
                item.transform.parent = newGridController.transform;
                TransitionMovement transitionMovement = item.gameObject.GetComponent<TransitionMovement>();
                transitionMovement.gridController = gridObject;
            }

            selectionState.currentState = SelectionState.sState.idle;

        }


        InstantiateNewUnit();
    }

    public void MovmentMain()
    {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 clickWorldPos = Camera.main.ScreenToWorldPoint(mousePos);


            newGridController = Instantiate(gridControllerPrefab, transform.position, transform.rotation);
            newGridController.transform.parent = gameObject.transform;
            gridObject = newGridController.GetComponent<GridController>();
            gridObject.worldPosition = clickWorldPos;
    }



    void InstantiateNewUnit()
    {
        if (Input.GetKeyDown(KeyCode.Space) && channel == 1)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject newUnit = Instantiate(unitPrefab, worldPosition, Quaternion.Euler(0,0,0));
            newUnit.transform.parent = gameObject.transform;
            newUnit.tag = unitID1;
        }
        else if (Input.GetKeyDown(KeyCode.C) && channel == 2)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject newUnit = Instantiate(unitPrefab, worldPosition, Quaternion.Euler(0,0,0));
            newUnit.transform.parent = gameObject.transform;
            newUnit.tag = unitID1;
        }
    }
}


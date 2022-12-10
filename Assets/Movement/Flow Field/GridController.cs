using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField currentFlowField;
    public Cell destinationCell;
    public GridDebug gridDebug;
    public Vector3 worldPosition;
    public GameObject idlecolliderprefab;

    public void InitializeFlowField()
    {
        
        currentFlowField = new FlowField(cellRadius, gridSize);
        currentFlowField.CreateGrid();
    }

 
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            InitializeFlowField();
            gridDebug.SetFlowField(currentFlowField);

            currentFlowField.CreateCostField();

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            destinationCell = currentFlowField.GetCellFromWorldPos(worldPosition);
            currentFlowField.CreateIntegrationField(destinationCell);

            currentFlowField.CreateFlowField();

            GameObject idlecollider = Instantiate(idlecolliderprefab);
            idlecollider.transform.parent = gameObject.transform;
            idlecollider.GetComponent<IdleMovementManger>().GenerateCollider(worldPosition, 30, 1f);



        }
        
    }
}
 
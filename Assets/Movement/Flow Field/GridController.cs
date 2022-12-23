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

    [HideInInspector]
    public Vector3 worldPosition;

    public void InitializeFlowField()
    {

        currentFlowField = new FlowField(cellRadius, gridSize);
        currentFlowField.CreateGrid();
    }

    private void Start()
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
    }
}
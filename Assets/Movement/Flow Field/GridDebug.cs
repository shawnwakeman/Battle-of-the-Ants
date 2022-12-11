using UnityEditor;
using UnityEngine;
 

public class GridDebug : MonoBehaviour
{
    public GridController gridController;
    public bool displayGrid;
 
 
    private Vector2Int gridSize;
    private float cellRadius;
    private FlowField curFlowField;
 
    private Sprite[] ffIcons;

    private void Start()
    {
        ffIcons = Resources.LoadAll<Sprite>("Sprites/FFicons");
        
    }
 
    public void SetFlowField(FlowField newFlowField)
    {
        curFlowField = newFlowField;
        cellRadius = newFlowField.cellRadius;
        gridSize = newFlowField.gridSize;
    }
    
    private void OnDrawGizmos()
    {
        if (displayGrid)
        {
            if (curFlowField == null)
            {
                DrawGrid(gridController.gridSize, Color.yellow, gridController.cellRadius);
            }
            else
            {
                DrawGrid(gridSize, Color.green, cellRadius);
            }
        }

        if (curFlowField == null) { return; }
 
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;

        if ( displayGrid)
        {
            foreach (Cell curCell in curFlowField.grid)
            {
                Handles.Label(curCell.worldPos, curCell.bestCost.ToString(), style);
            }            
        }



        
    }
    


    private void DrawGrid(Vector2Int drawGridSize, Color drawColor, float drawCellRadius)
    {
        Gizmos.color = drawColor;
        for (int x = 0; x < drawGridSize.x; x++)
        {
            for (int y = 0; y < drawGridSize.y; y++)
            {
                Vector3 center = new Vector3(drawCellRadius * 2 * x + drawCellRadius, drawCellRadius * 2 * y + drawCellRadius, 0);
                Vector3 size = Vector3.one * drawCellRadius * 2;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
}
 
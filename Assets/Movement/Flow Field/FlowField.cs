using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    public Cell[,] grid {get; private set;}
    public Vector2Int gridSize {get; private set;}

    public float cellRadius {get; private set;}
    public Cell destinationCell;
    private float cellDiameter;

    public FlowField(float _cellRadius, Vector2Int _gridSize)
    {
        cellRadius = _cellRadius;
        gridSize = _gridSize;
        cellDiameter = cellRadius * 2f;
        

    }

    public void CreateGrid()
    {
        grid = new Cell[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2 worldPos = new Vector2(cellDiameter * x + cellRadius, cellDiameter * y + cellRadius);
                grid[x,y] = new Cell(worldPos, new Vector2Int(x,y));
            }
        }
    }


    public void CreateCostField()
    {
        Vector3 cellHalfExtents = Vector3.one * cellRadius;
        int terrainMask = LayerMask.GetMask("impassable", "slowTerrain");
        foreach (Cell curCell in grid)
        {
            Collider2D[] obstacles = Physics2D.OverlapBoxAll(curCell.worldPos, cellHalfExtents, 0.0f);
            bool hasIncreasedCost = false;
            foreach (Collider2D col in obstacles)
            {
                if (col.gameObject.layer == 8)
                {
                    curCell.IncreaseCost(255);
                    continue;
                }
                else if (!hasIncreasedCost && col.gameObject.layer == 9)
                {
                    curCell.IncreaseCost(3);
                    hasIncreasedCost = true;
                }
            }
        }
    }

    public void CreateFlowField()
    {
        foreach(Cell curCell in grid)
        {
            List<Cell> curNeighbors = GetNeighborCells(curCell.gridIndex, GridDirection.AllDirections);
 
            int bestCost = curCell.bestCost;
 
            foreach(Cell curNeighbor in curNeighbors)
            {
                if(curNeighbor.bestCost < bestCost)
                {
                    bestCost = curNeighbor.bestCost;
                    curCell.bestDirection = GridDirection.GetDirectionFromV2I(curNeighbor.gridIndex - curCell.gridIndex);
                    
                }
            }
        }
    }
    
    public void CreateIntegrationField(Cell _destinationCell)
    {
        destinationCell = _destinationCell;
 
        destinationCell.cost = 0;
        destinationCell.bestCost = 0;
 
        Queue<Cell> cellsToCheck = new Queue<Cell>();
 
        cellsToCheck.Enqueue(destinationCell);
 
        while(cellsToCheck.Count > 0)
        {
            Cell curCell = cellsToCheck.Dequeue();
            List<Cell> curNeighbors = GetNeighborCells(curCell.gridIndex, GridDirection.CardinalDirections);
            foreach (Cell curNeighbor in curNeighbors)
            {
                if (curNeighbor.cost == byte.MaxValue) { continue; }
                if (curNeighbor.cost + curCell.bestCost < curNeighbor.bestCost)
                {
                    curNeighbor.bestCost = (ushort)(curNeighbor.cost + curCell.bestCost);
                    cellsToCheck.Enqueue(curNeighbor);
                }
            }
        }
    }

 
    private List<Cell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
    {
        List<Cell> neighborCells = new List<Cell>();
 
        foreach (Vector2Int curDirection in directions)
        {
            Cell newNeighbor = GetCellAtRelativePos(nodeIndex, curDirection);
            if (newNeighbor != null)
            {
                neighborCells.Add(newNeighbor);
            }
        }
        return neighborCells;
    }
 
    private Cell GetCellAtRelativePos(Vector2Int orignPos, Vector2Int relativePos)
    {
        Vector2Int finalPos = orignPos + relativePos;
 
        if (finalPos.x < 0 || finalPos.x >= gridSize.x || finalPos.y < 0 || finalPos.y >= gridSize.y)
        {
            return null;
        }
 
        else { return grid[finalPos.x, finalPos.y]; }
    }
    

    public Cell GetCellFromWorldPos(Vector3 worldPos )
    {
        
        float x = worldPos.x;
        float y = worldPos.y;

        x = x/cellDiameter;
        y = y/cellDiameter;

        x = Mathf.Ceil(x);
        y = Mathf.Ceil(y);
        return grid[(int)x-1, (int)y-1];
    }
}
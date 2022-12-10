using UnityEngine;

public class Cell
{
    public Vector2 worldPos;
    public Vector2Int gridIndex;
    public ushort bestCost;
    public byte cost;
    public GridDirection bestDirection;

    public Cell(Vector2 _worldpos, Vector2Int _gridindex)  
    {
        worldPos = _worldpos;
        gridIndex = _gridindex;
        cost = 1;
        bestCost = ushort.MaxValue;
        bestDirection = GridDirection.None;

    }

    public void IncreaseCost(int amount)
    {
        if (cost == byte.MaxValue){ return; }
        if (amount + cost >= byte.MaxValue){ cost = byte.MaxValue; }
        else{ cost += (byte)amount; }
    }
}

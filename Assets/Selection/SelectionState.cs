using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionState : MonoBehaviour
{
    // Start is called before the first frame update
    public PolygonCollider2D polygonCollider2D;
    public CursorPath cursorPath;
    public sState currentState = sState.idle;
    public bool PrimedOnce;
    public enum sState
    {
        justStartedSelection,
        currentlySelecting,
        finishedSelecting,
        idle,
        primedForTarget,
        clickedToTarget,

    }

    private void Update()
    {
        DetermineSelectionState();
    }

    void DetermineSelectionState()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentState = sState.justStartedSelection;
        }
        else if (Input.GetMouseButton(0))
        {
            currentState = sState.currentlySelecting;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentState = sState.finishedSelecting;
            PrimedOnce = true;
        }
        else
        {
            currentState = sState.idle;
            checkForPrimedState();
        }
        
    }

    public void checkForPrimedState()
    {
        if (PrimedOnce)
        {
            if (cursorPath.futurePoints.Count > 1 && currentState == sState.idle)
            {
                currentState = sState.primedForTarget;
            }       
        }


    }


}

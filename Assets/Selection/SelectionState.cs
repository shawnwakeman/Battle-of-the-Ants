using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionState : MonoBehaviour
{
    [Range(0, 1)]
    public float clickHoldThreshold;
    float timeafterclick;
    public PolygonCollider2D polygonCollider2D;
    public CursorPath cursorPath;
    public sState currentState = sState.idle;
    public iState currentIState = iState.readyForInput;
    public bool PrimedOnce;
    public bool clicked;
    public EdgeCollisionDetection edgeCollisionDetection;
    public int curretMouseButton;
    public enum sState

    {
        justStartedSelection,
        currentlySelecting,
        holding,
        finishedSelecting,
        idle,
        clickedToTarget,
        dragingToTarget,

    }

    public enum iState
    {
        readyForInput,
        primedForTarget,

    }
    
    private void Start() 
    {
        edgeCollisionDetection = polygonCollider2D.GetComponent<EdgeCollisionDetection>();
        
    }

    private void Update()
    {
        DetermineSelectionState();
    }

    void DetermineSelectionState()
    {
        if (Input.GetMouseButtonDown(curretMouseButton))
        {
            currentState = SelectionState.sState.justStartedSelection;
        }
        else if (Input.GetMouseButton(curretMouseButton))
        {
            currentState = sState.currentlySelecting;
            timeafterclick += Time.deltaTime;
            if (timeafterclick > clickHoldThreshold)
            {
                // Debug.Log("Being Held");
                // can be used to count clicks
                
            }
        }
        else if (Input.GetMouseButtonUp(curretMouseButton))
        {
            currentState = sState.finishedSelecting;
            
            if (timeafterclick < clickHoldThreshold)
            {
                // Debug.Log("ClickedNotHeld");
                if (currentIState == iState.primedForTarget)
                {
                    currentState = sState.clickedToTarget;
                }
            }
            timeafterclick = 0;
        }
        else
        {
            if (!(currentState == sState.clickedToTarget))
            {
                currentState = sState.idle;
            }
            checkForPrimedState();
        }
    }


    public void checkForPrimedState()
    {

        if (edgeCollisionDetection.overlappingColliders.Count > 0)
        {
            currentIState = iState.primedForTarget;

        }
        else
        {
            currentIState = iState.readyForInput;
        }       



    }


}


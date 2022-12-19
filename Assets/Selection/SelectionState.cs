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
        if (Input.GetMouseButtonDown(0))
        {
            currentState = SelectionState.sState.justStartedSelection;
        }
        else if (Input.GetMouseButton(0))
        {
            currentState = sState.currentlySelecting;
            timeafterclick += Time.deltaTime;
            if (timeafterclick > clickHoldThreshold)
            {
                Debug.Log("Being Held");
                
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentState = sState.finishedSelecting;
            
            if (timeafterclick < clickHoldThreshold)
            {
                Debug.Log("ClickedNotHeld");
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


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SelectionState : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public PolygonCollider2D polygonCollider2D;
//     public CursorPath cursorPath;
//     public sState currentState = sState.idle;
//     public bool PrimedOnce;
//     public bool clickedAfterPrime;
//     public EdgeCollisionDetection edgeCollisionDetection;
//     public enum sState

//     {
//         justStartedSelection,
//         currentlySelecting,
//         finishedSelecting,
//         idle,
//         primedForTarget,
//         clickedToTarget,    

//     }
    
//     private void Start() 
//     {
//         edgeCollisionDetection = polygonCollider2D.GetComponent<EdgeCollisionDetection>();
//     }

//     private void Update()
//     {
//         DetermineSelectionState();
//     }

//     void DetermineSelectionState()
//     {
//         if(Input.GetMouseButtonDown(0))
//         {
//             if (currentState == sState.primedForTarget)
//             {
//                 clickedAfterPrime = true;

//             }
//             currentState = sState.justStartedSelection;
//         }
//         else if (Input.GetMouseButton(0))
//         {
//             currentState = sState.currentlySelecting;
//         }
//         else if (Input.GetMouseButtonUp(0))
//         {
//             List<Collider2D> checkForChanges = edgeCollisionDetection.overlappingColliders;
//             currentState = sState.finishedSelecting;
//             PrimedOnce = true;
//             // if (clickedAfterPrime && CheckMatch(checkForChanges, edgeCollisionDetection.overlappingColliders) && currentState == sState.primedForTarget)
//             // {
//             //     currentState = sState.clickedToTarget;
//             //     Debug.Log("was ran");
//             //     Debug.Assert(true);
//             // }    
//         }

//         checkForPrimedState();

        
//     }

//     bool CheckMatch(List<Collider2D> checkForChanges, List<Collider2D> overlappingColliders) 
//     {
//         if (checkForChanges.Count != overlappingColliders.Count) { return false; }
//         for (int i = 0; i < checkForChanges.Count; i++) 
//         {
//             if (checkForChanges[i] != overlappingColliders[i]) { return false; }
//         }
//         return true;
//     }

//     public void checkForPrimedState()
//     {
//         if (PrimedOnce)
//         {
//             if (cursorPath.futurePoints.Count > 1 &&
//             currentState == sState.finishedSelecting && 
//             edgeCollisionDetection.overlappingColliders.Count > 0)
//             {
//                 currentState = sState.primedForTarget;
//             }
//             else if (currentState == sState.finishedSelecting && edgeCollisionDetection.overlappingColliders.Count == 0)
//             {
//                 currentState = sState.idle;
//             }    
//         }


//     }


// }

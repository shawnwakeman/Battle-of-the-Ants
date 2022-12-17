using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionState : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Transform> transforms;
    public Transform[] arrayOfTransforms;
    public sState currentState = sState.idle;
    public enum sState
    {
        justStartedSelection,
        currentlySelecting,
        finishedSelecting,
        idle,
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
        }
        else
        {
            currentState = sState.idle;
        }
    }


    public void SetTransforms(List<GameObject> transformList)
    {
        foreach (GameObject transfromObject in transformList)
        {
            Transform transformToAdd = transfromObject.transform;
            transforms.Add(transformToAdd);
            Debug.Log(transformToAdd);
        }

        arrayOfTransforms = transforms.ToArray();
        
    }



}

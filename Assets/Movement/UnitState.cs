using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationDistance;
    public float orbitLevel;
    public uState intarget;




    public enum uState
    {
        idle,
        transition,
        calculatingIdle,
        inTargetRange,
        

    }

    private void Start() 
    {
        // orbitLevel = Random.Range(1.2f, 2.2f);
        
    }

    public uState currentStateInt = uState.transition;

    public void SetState(uState desiredState)
    {
        currentStateInt = desiredState;
    }




}

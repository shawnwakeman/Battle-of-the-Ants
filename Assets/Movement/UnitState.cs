using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationDistance;
    public enum uState
    {
        idle,
        transition,
        calculatingIdle,
    }

    public uState currentStateInt = uState.transition;

    public void SetState(uState desiredState)
    {
        currentStateInt = desiredState;
    }



}

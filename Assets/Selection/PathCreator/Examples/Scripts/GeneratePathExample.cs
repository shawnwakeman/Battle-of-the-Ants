using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples {
    // Example of creating a path at runtime from a set of points.

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        public bool closedLoop = true;
        public Transform[] waypoints;

        public SelectionState selectionState;
        BezierPath bezierPath;


        private void Start() {
            
        }

        void Update () 
        {
            waypoints = selectionState.arrayOfTransforms;
            if (waypoints != null) {
                if (waypoints.Length > 1)
                {
                    // Create a new bezier path from the waypoints.
                    bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xyz);
                    GetComponent<PathCreator> ().bezierPath = bezierPath;   
                     
                                
                }
            }
            if (selectionState.currentState == SelectionState.sState.justStartedSelection)
            {
                
                bezierPath = null;
                selectionState.arrayOfTransforms = null;
                waypoints = null;
                foreach (Transform transfromToBeDestroyed in selectionState.transforms)
                {
                    Destroy(transfromToBeDestroyed.gameObject);
                }
                selectionState.transforms.Clear();
            }       
        }
    }


}
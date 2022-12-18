using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollisionDetection : MonoBehaviour
{
    public List<Collider2D> overlappingColliders = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collider) {
        if(!overlappingColliders.Contains(collider)) {
            overlappingColliders.Add(collider);
            Debug.Log(collider.gameObject.name);
        }
    }



}

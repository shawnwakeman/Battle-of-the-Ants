using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollisionDetection : MonoBehaviour
{
    public string unitID;

    public List<Collider2D> overlappingColliders = new List<Collider2D>();
    public List<Collider2D> overlappingHubs = new List<Collider2D>();
    private void Start() 
    {
        overlappingColliders.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!overlappingColliders.Contains(collider))
        {
            if (collider.gameObject.CompareTag(unitID))
            {
                overlappingColliders.Add(collider);
            }

            if (collider.gameObject.CompareTag("hub"))
            {
                overlappingHubs.Add(collider);
                collider.GetComponent<AntHillManager>().selected = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        overlappingColliders.Remove(collider);
        overlappingHubs.Remove(collider);
    }
}

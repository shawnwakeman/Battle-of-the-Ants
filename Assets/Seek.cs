using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public UnitState unitState;
    public Rigidbody2D agentRB;
    public List<Collider2D> overlappingUnitColliders = new List<Collider2D>();
    public string enemy;

    bool destructionKey;
    void Start()
    {
        destructionKey = true;
        if (gameObject.transform.parent.CompareTag("unit"))
        {
            enemy = "unit2";
            
        }
        else 
        {
            enemy = "unit";
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (overlappingUnitColliders.Count > 0)
        {
            GameObject enemytarget = overlappingUnitColliders[0].gameObject;
            if (destructionKey)
            {
                Destroy(enemytarget);
                destructionKey = false;
            }
        }
     
    }

    private void OnTriggerEnter2D(Collider2D scanner) {
        if(!overlappingUnitColliders.Contains(scanner)) {
            if (scanner.gameObject.CompareTag(enemy))
            {
                overlappingUnitColliders.Add(scanner);
                Debug.Log(scanner.gameObject.name);                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D scanner) 
    {
        overlappingUnitColliders.Remove(scanner);
    }
}

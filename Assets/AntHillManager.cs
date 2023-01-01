using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntHillManager : MonoBehaviour
{
    public int antCount;
    public bool selected;    
    [SerializeField] SelectionState selectionState;

    [SerializeField] MovementManager movementManager;
    [SerializeField] string unitID1;

    [SerializeField] GameObject unitPrefab;

    [SerializeField] GameObject enemyObject;

    [SerializeField] SpriteRenderer spriteRenderer;




    string enemyString;

    private void Start() 
    {

        
    }



    // Update is called once per frame
    void Update()
    {

        if (unitID1 == "unit")
        {
            enemyString = "unit2";
            spriteRenderer.color = Color.cyan;
        }
        else if (unitID1 == "unit2")
        {
            enemyString = "unit";
            spriteRenderer.color = Color.yellow;
        }

        if (selectionState.currentState == SelectionState.sState.clickedToTarget && selected)
        {
            
            for (int i = 0; i <= antCount; i++)
            {
                //should use getter
                movementManager.MovmentMain();
                if (i > 0)
                {
                    GameObject item = Instantiate(unitPrefab, gameObject.transform.position + new Vector3(2, 0, 0), gameObject.transform.rotation);
                    item.transform.parent = gameObject.transform;
                    item.tag = unitID1;
                    item.transform.parent = movementManager.newGridController.transform;
                    item.gameObject.GetComponent<TransitionMovement>().gridController = movementManager.gridObject; // bad
                    
                }
            }
            selected = false;
            antCount = 0; 
        }

        if (antCount < 0)   
        {
            
            if (unitID1 == "unit")
            {
                unitID1 = "unit2";
                antCount = 0; 
                
            }
            else if (unitID1 == "unit2")
            {
                unitID1 = "unit";
                antCount = 0; 
                
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag(unitID1))
        {
            if (Vector2.Distance(other.gameObject.GetComponent<TransitionMovement>().gridController.worldPosition,
                other.gameObject.GetComponent<Rigidbody2D>().position) < 1)
            {
                Destroy(other.gameObject);
                antCount++;
            }
        } 
        else if (other.gameObject.CompareTag(enemyString))
        {
            Destroy(other.gameObject);
            antCount--;
        } 

        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag(unitID1))
        {
            if (Vector2.Distance(other.gameObject.GetComponent<TransitionMovement>().gridController.worldPosition,
                other.gameObject.GetComponent<Rigidbody2D>().position) < 1)
            {
                Destroy(other.gameObject);
                antCount++;
            }
        } 
        else if (other.gameObject.CompareTag(enemyString))
        {
            Destroy(other.gameObject);
            antCount--;
        } 

        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        
    }
}

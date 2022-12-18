using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public List<GameObject> activeList = new List<GameObject>();
    public GridController gridController;
    public GameObject unitPrefab;



    void Start()
    {
        foreach ( Transform unit in transform)
        {
            if (unit.tag == "unit")
            {
                activeList.Add(unit.gameObject);
            }
        }

        foreach (GameObject unit in activeList)
        {
            Debug.Log(unit);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject unit in activeList)
            {
                unit.GetComponent<UnitState>().currentStateInt = UnitState.uState.transition;
            }
        }
        InstantiateNewUnit();
    }



    public void InstantiateNewUnit()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject newUnit = Instantiate(unitPrefab, worldPosition, Quaternion.Euler(0,0,0));
            newUnit.GetComponent<IdleMovement>().gridController = gridController;
            newUnit.GetComponent<TransitionMovement>().gridController = gridController;
            newUnit.transform.parent = gameObject.transform;
            activeList.Add(newUnit);

        }
    }
}


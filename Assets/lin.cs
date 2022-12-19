using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lin : MonoBehaviour
{

    [Range(0, 1)]
    public float clickHoldThreshold;
    float timeafterclick;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
        else if (Input.GetMouseButton(0))
        {
            timeafterclick += Time.deltaTime;
            if (timeafterclick > clickHoldThreshold)
            {
                Debug.Log("Being Held");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {   

            timeafterclick = 0;
            if (timeafterclick < clickHoldThreshold)
            {
                Debug.Log("ClickedNotHeld");
            }
        }
        
    }

}

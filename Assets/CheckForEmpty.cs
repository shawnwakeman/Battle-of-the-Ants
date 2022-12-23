using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEmpty : MonoBehaviour
{
    // Start is called before the first frame update


    

    // Update is called once per frame
    void Update()
    {
        List<Transform> taggedChildren = new List<Transform>();
 
        foreach(Transform child in transform)
        {
            if(child.tag == "unit") taggedChildren.Add(child);
        }

        if (taggedChildren.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}

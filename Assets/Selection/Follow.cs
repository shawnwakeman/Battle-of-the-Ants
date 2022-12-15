using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follow : MonoBehaviour
{
   public PathCreator pathCreator;
   public float speed;
   float dstTravelled;

   void Update()
   {
       dstTravelled += speed * Time.deltaTime;
       transform.position = pathCreator.path.GetPointAtDistance(dstTravelled);
       transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled);
       
   }
}

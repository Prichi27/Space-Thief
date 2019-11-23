using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camfollow : MonoBehaviour
{
   public Transform target;
   public float smoothspeed;

   void LateUpdate (){
   
   transform.position = target.position;
   }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    Rigidbody rb;
    CapsuleCollider capCol;
    Animator EnemyAnim;
 private float moveSpeed = 10.0f;
    public GameObject CharPos;
    private float Distance;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        EnemyAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        

		 //locate woobie's location
        CharPos = GameObject.Find("Woobie");
        Distance = this.transform.position.z - CharPos.transform.position.z;


      
        
        if (Distance < 30f)
        {
            EnemyAnim.SetTrigger("IsRunning");
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

		
    }
        
   
}

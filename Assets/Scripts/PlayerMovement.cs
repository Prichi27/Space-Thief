using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float runspeed;
    public float WalkSpeed;
    public float rotationspeed;

	 private float moveSpeed = 0.3f;
    public GameObject CharPos;
    private float Distance;

    Rigidbody rb;
    CapsuleCollider capCol;
    Animator WoobieAnim;



    // Start is called before the first frame update
    void Start()
    {
	 rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        WoobieAnim = GetComponent<Animator>();

		
        
    }

    // Update is called once per frame
    void Update()
    {
          transform.Translate(0, 0, Input.GetAxis("Vertical") * speed);

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotationspeed, 0);

       transform.Translate(0, 0, Input.GetAxis("Horizontal") * speed);

		
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            speed = WalkSpeed;
            WoobieAnim.SetBool("IsWalking", true);
            WoobieAnim.SetBool("IsIdle", false);
			

       
        }

        else {
            WoobieAnim.SetBool("IsWalking", false);
           WoobieAnim.SetBool("IsIdle", true);
            speed = 0;
        }

		
		 //locate Target's location
        CharPos = GameObject.Find("Enemy");
        Distance = this.transform.position.z - CharPos.transform.position.z;


      
        
        if (Distance < 30f)
        {
           speed= moveSpeed;

		               
        }

		
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    //AudioSource m_AudioSource;

    public float turnSpeed = 20f;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;

    private float _moveSpeed;
    private bool playerCanSteal;
    private bool canRun;

	public GameObject GrabCanvas;

	//crownobjects
	public GameObject CrownTrees;
	public GameObject CrownBuilding;
	public GameObject CrownObs;
	public GameObject CrownClouds;
	public GameObject CrownHouse;

	//Enemyobjects
	public GameObject EnemyTrees;
	public GameObject EnemyBuilding;
	public GameObject EnemyObs;
	public GameObject EnemyClouds;
	public GameObject EnemyHouse;

	//UIobjects
	public GameObject UITrees;
	public GameObject UIBuilding;
	public GameObject UIObs;
	public GameObject UIClouds;
	public GameObject UIHouse;




    //public FixedJoystick fixedJoystick;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_AudioSource = GetComponent<AudioSource>();

		GrabCanvas.SetActive(false);

		CrownTrees.SetActive(false);
		CrownBuilding.SetActive(false);
		CrownClouds.SetActive(false);
		CrownObs.SetActive(false);
		CrownHouse.SetActive(false);

		EnemyTrees.SetActive(true);
		EnemyBuilding.SetActive(true);
		EnemyClouds.SetActive(true);
		EnemyObs.SetActive(true);
		EnemyHouse.SetActive(true);

		UITrees.SetActive(true);
		UIBuilding.SetActive(true);
	    UIClouds.SetActive(true);
		UIObs.SetActive(true);
		UIHouse.SetActive(true);

    }

    void FixedUpdate()
    {
        //float horizontal = fixedJoystick.Horizontal;
        //float vertical = fixedJoystick.Vertical;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);



        bool isWalking = (hasHorizontalInput || hasVerticalInput) && !canRun;
        bool isRunning = (hasHorizontalInput || hasVerticalInput) && canRun;

        m_Animator.SetBool("IsWalking", isWalking);
        m_Animator.SetBool("IsRunning", isRunning);

        //if (isWalking)
        //{
        //    if (!m_AudioSource.isPlaying)
        //    {
        //        m_AudioSource.Play();
        //    }
        //}
        //else
        //{
        //    m_AudioSource.Stop();
        //}

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        if (canRun)
        {
            _moveSpeed = 20f;
        }

        else
        {
            _moveSpeed = 10f;
        }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * _moveSpeed * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerCanSteal = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && playerCanSteal )
        {
		GrabCanvas.SetActive(true);

		//display E 
            if(Input.GetKeyUp(KeyCode.E))
            {

			
		

                //Stealing occurs here, 5 if statements to differentiate between enemies

				 if  (other.gameObject.name == "BuildingPlanet") 
				 {
				    Debug.Log("Build");
					CrownBuilding.SetActive(true);
					EnemyBuilding.SetActive(false);
					UIBuilding.SetActive(false);
					
			
				 }
				  if  (other.gameObject.name == "ObservPlanet") 
				 {
				    Debug.Log("Obs");
					CrownObs.SetActive(true);
					EnemyObs.SetActive(false);
					UIObs.SetActive(false);
					
				 }
				  if  (other.gameObject.name == "TreePlanet") 
				 {
				    Debug.Log("Tree");
					CrownTrees.SetActive(true);
					EnemyTrees.SetActive(false);
					UITrees.SetActive(false);
					
				 }
				  if  (other.gameObject.name == "HousePlanet") 
				 {
				    Debug.Log("house");
					CrownHouse.SetActive(true);
					EnemyHouse.SetActive(false);
					UIHouse.SetActive(false);
					
				 }
				  if  (other.gameObject.name == "CloudPlanet") 
				 {
				    Debug.Log("cloud");
					CrownClouds.SetActive(true);
					EnemyClouds.SetActive(false);
					UIClouds.SetActive(false);
					
				 } 
				 			GrabCanvas.SetActive(false);

			
              //  Debug.Log("yeah");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerCanSteal = false;
			GrabCanvas.SetActive(false);

        }
    }

    public void CanPlayerRun(bool value)
    {
        canRun = value;
    }



}

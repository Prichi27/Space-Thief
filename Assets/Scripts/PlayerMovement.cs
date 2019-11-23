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


    //public FixedJoystick fixedJoystick;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_AudioSource = GetComponent<AudioSource>();
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
            if(Input.GetKeyUp(KeyCode.E))
            {
                // Stealing occurs here
                Debug.Log("yeah");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerCanSteal = false;
        }
    }

    public void CanPlayerRun(bool value)
    {
        canRun = value;
    }



}

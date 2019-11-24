using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Variables set via inspector
    [SerializeField]
    [Tooltip("Distance fleed by enemy!!!")]
    [Range(0.1f,5)]
    private float distanceFactor;

    public GameObject stolenObject;

    Rigidbody _rb;
    Animator _enemyAnim;

    private bool shouldRun;
    private bool _isRunning;
    private Vector3 runningDirection;
    private NavMeshAgent _agent;

    // Audio
    private AudioSource _audioSource;
    private bool _isAudioPlaying;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _enemyAnim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _isAudioPlaying = false;
    }

    private void LateUpdate()
    {

        if (!_agent.pathPending && _isRunning)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    _enemyAnim.SetBool("IsRunning", false);
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _enemyAnim.SetBool("IsRunning", true);
            shouldRun = true;
            _isRunning = true;

            // Set Player Animation to run
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.CanPlayerRun(true);

            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(shouldRun && other.tag.Equals("Player"))
        {
            Vector3 dirToPlayer = transform.position - other.transform.position;
            Vector3 newPos = transform.position + dirToPlayer * distanceFactor;
            _agent.SetDestination(newPos);
            //PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            //playerMovement.CanPlayerRun(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            shouldRun = false;

            // Set Player Animation to walk again
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.CanPlayerRun(false);
            
            if (!stolenObject.activeSelf)
            {
                _agent.isStopped = true;
                _enemyAnim.SetTrigger("Dead");
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Variables set via inspector
    [SerializeField]
    private float _moveSpeed = 10.0f;

    Rigidbody _rb;
    Animator _enemyAnim;

    private bool shouldRun;
    private bool _isRunning;
    private Vector3 runningDirection;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _enemyAnim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(shouldRun)
        {
            Vector3 dirToPlayer = transform.position - other.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            _agent.SetDestination(newPos);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            shouldRun = false;
        }
    }

}

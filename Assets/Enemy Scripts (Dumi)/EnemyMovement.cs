using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float targetStopDistance = 1.5f;
    public float followTime = 0.1f;
    public float aiUpdateTick = 1;
    public bool isActive;

    public LayerMask includeOnPlayerCheck;

    public Transform[] patrolPoints;
    private int currentPatrolIndex;
    public float patrolWaitTime = 2f;
    private bool isPatrolling;
    private bool waiting;

    private NavMeshAgent _navMeshAgent;
    private GameObject _player;

    private bool _chasePlayer;
    private bool _lookingForPlayer;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent.stoppingDistance = _player.GetComponent<CharacterController>().bounds.extents.x + targetStopDistance;

        isPatrolling = true;
        currentPatrolIndex = 0;
        MoveToNextPatrolPoint();
        _navMeshAgent.speed = 3.5f; // Adjust the patrol speed

        Debug.Log("Enemy started patrolling.");
    }

    void Update()
    {
        if (isActive)
        {
            if (_chasePlayer)
            {
                ChasePlayer();
            }
            else if (isPatrolling)
            {
                Patrol();
            }
        }

        CheckForPlayer();
    }

    void Patrol()
    {
        if (waiting) return;

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
        {
            Debug.Log("Enemy reached patrol point.");
            StartCoroutine(PatrolWait());
        }
    }

    IEnumerator PatrolWait()
    {
        waiting = true;
        Debug.Log("Enemy waiting at patrol point.");
        yield return new WaitForSeconds(patrolWaitTime);
        MoveToNextPatrolPoint();
        waiting = false;
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        _navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        Debug.Log("Enemy moving to next patrol point.");
    }

    void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= 10f) // Adjust the detection range
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, _player.transform.position - transform.position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, includeOnPlayerCheck))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    _chasePlayer = true;
                    isPatrolling = false;

                    Debug.Log("Player detected. Enemy starts chasing.");

                    if (!_lookingForPlayer)
                    {
                        StartCoroutine(FollowCountDown());
                    }
                }
            }
        }
    }

    void ChasePlayer()
    {
        _navMeshAgent.speed = 6f; // Adjust the chase speed
        _navMeshAgent.SetDestination(_player.transform.position);

        Debug.Log("Enemy chasing player.");

        if (Vector3.Distance(transform.position, _player.transform.position) <= _navMeshAgent.stoppingDistance)
        {
            // Attack logic here
            Debug.Log("Enemy in range to attack.");
        }
    }

    IEnumerator FollowCountDown()
    {
        _lookingForPlayer = true;
        yield return new WaitForSeconds(followTime);
        _chasePlayer = false;
        isActive = false;
        _lookingForPlayer = false;
        isPatrolling = true;
        MoveToNextPatrolPoint();

        Debug.Log("Enemy lost player. Resuming patrol.");
    }
}

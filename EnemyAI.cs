using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    public Transform player;
    [SerializeField] private Transform[] wayPoints;
    public float lookRadius;
    [Range(0, 360)] public float lookAngle;
    [SerializeField] float movementSpeed;
    [SerializeField] private LayerMask targetMask, obstructionMask;
    private int index = 0;
    private Vector3 targetWayPoint;

    private bool isAttacking = false, isPatrolling, patrolReturning = false;
    public bool detectedPlayer = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {   
        // Updating target Waypoints
        if(Vector3.Distance(transform.position, targetWayPoint) < 0.5f)
        {
            if(!patrolReturning)
            {
                UpdateIndex();
            }
            else if(patrolReturning)
            {
                UpdateInverseIndex();
            }
        }
        UpdateDestination(); //Updating destination of enemy
    }

    private void UpdateDestination()
    {
        targetWayPoint = wayPoints[index].position;
        
        if(detectedPlayer)
        {
            Debug.Log("detected player");
            navAgent.stoppingDistance = 1.0f;
            navAgent.SetDestination(player.position);
        }
        else 
        {
            Debug.Log("not detected");
            navAgent.stoppingDistance = 0.0f;
            navAgent.SetDestination(targetWayPoint);
        }
    }

    private void UpdateIndex()
    {
        index++; Debug.Log("increasing index");
        if(index == wayPoints.Length)
        {
            patrolReturning = true;
        }
    }

    private void UpdateInverseIndex()
    {
        index--; Debug.Log("inversing index");
        if(index == 0)
        {
            patrolReturning = false;
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, lookRadius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.position, directionToTarget) < lookAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    detectedPlayer = true;
                else
                    detectedPlayer = false;
            }
            else
                detectedPlayer = false;
        }
        else if(detectedPlayer)
            detectedPlayer = false;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

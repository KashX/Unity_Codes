using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;
    EnemyStateFactory states;

    //_____________...getter and setter..._______________________________________________________________________________________
    public EnemyBaseState CurrentState {get {return currentState;} set {currentState = value;}}
    public NavMeshAgent NavAgent {get {return navAgent;}}
    public Animator Animator {get {return animator;}}
    public Transform Player {get {return player;}}
    public Transform[] WayPoints {get {return wayPoints;} set{wayPoints = value;}}
    public Vector3 TargetWayPoint {get {return targetWayPoint;} set{targetWayPoint = value;}}
    public int Index {get {return index;} set{index = value;}}
    public float ChaseSpeed {get {return chaseSpeed;}}
    public float PatrolSpeed {get {return patrolSpeed;}}
    public float LookRadius {get {return lookRadius;}}
    public float LookAngle {get {return lookAngle;}}
    public float CurrentTime {get {return currentTime;} set{currentTime = value;}}
    public float DelayTime {get {return delayTime;} set{delayTime = value;}}
    public bool DetectedPlayer {get {return detectedPlayer;} set {detectedPlayer = value;}}


    //_____________...All Variables needed..._____________________________________________________________________________________
    private NavMeshAgent navAgent;
    private Animator animator;

    [SerializeField] private Transform[] wayPoints;

    [Header("Detection Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private VisualEffect poisonAttack;
    [SerializeField] private float lookRadius;
    [SerializeField] [Range(0, 360)] private float lookAngle;
    [SerializeField] private LayerMask targetMask, obstructionMask;

    [Header("Movement Speed")]
    [SerializeField] private float chaseSpeed = 3.0f;
    [SerializeField] private float patrolSpeed = 2.0f;
    private int index = 0;
    private Vector3 targetWayPoint;

    private bool isPatrolling;
    private bool detectedPlayer = false;

    // [Header("Attack Settings")]
    private float currentTime = 0.0f;
    private float delayTime = 3.0f;

    

    void Awake()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();    
    }

    void Start()
    {
        //States Setup
        states = new EnemyStateFactory(this);
        currentState = states.Patrol();
        currentState.EnterState();
        
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        currentState.UpdateState();
    }

    private void SpawnPoisonVFX()
    {
        VisualEffect vfx = Instantiate(poisonAttack, transform.position, transform.rotation);
        vfx.Play();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, lookRadius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < lookAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        // Debug.Log("detected player!!!.....................!!!");
                        detectedPlayer = true;
                    }
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
}

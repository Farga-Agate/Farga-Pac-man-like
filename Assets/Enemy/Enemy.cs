using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private BaseState _currentState;

    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    public List<Transform> WayPoints => wayPoints;

    [SerializeField] private float chaseDistance;
    public float ChaseDistance => chaseDistance;

    [SerializeField] private Player player;
    public Player Player => player;
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }
    private void Awake()
    {
        _currentState = PatrolState;
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (player != null)
        {
            player.OnPowerUpStart += StartRetreating;
            player.OnPowerUpStop += StopRetreating;
        }
    }
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_currentState == RetreatState)
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Dead();
        }
    }
}

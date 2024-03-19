using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent), typeof(Health), typeof(HumanoidController))]
public class EnemyWithStates : MonoBehaviour
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float damage = 30;
    [SerializeField] private float viewAngle;
    [SerializeField] private PlayerStats stats;
    
    public static int enemyCount;
    
    public List<Transform> patrolPoints;
    
    private PlayerCharacter _player;
    private NavMeshAgent _navMeshAgent;
    private Health _myHealth;
    private State _currentState;
    private HumanoidController _controller;

    private void Awake()
    {
        _myHealth = GetComponent<Health>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _controller = GetComponent<HumanoidController>();
        var animator = GetComponentInChildren<Animator>();
        animator.SetLayerWeight(animator.GetLayerIndex("PistolAim"), 0);
    }

    private void OnEnable()
    {
        enemyCount += 1;
        _myHealth.ZeroHealth += DieDieMyDarling;
        _player = FindObjectOfType<PlayerCharacter>(true);
        _currentState = new PatrolState();
    }

    private void OnDisable()
    {
        _myHealth.ZeroHealth -= DieDieMyDarling;
        enemyCount -= 1;
    }

    private void OnDestroy()
    {
        stats.AddExp(100);
    }

    private void DieDieMyDarling()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if(_currentState != null) _currentState.Tick(this);
    }

    private void SwitchState(State to)
    {
        _currentState.OnExit(this);
        _currentState = to;
        _currentState.OnEnter(this);
    }

    private Vector3 PickRandomPatrolPoint() => patrolPoints[Random.Range(0, patrolPoints.Count)].position;

    private bool IsWithinAttackRange => Vector3.Distance(transform.position, _player.transform.position) <= attackDistance;

    private bool IsPlayerNoticed()
    {
        if (!_player) return false;
        var direction = _player.transform.position - transform.position;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            if (Physics.Raycast(transform.position + Vector3.up, direction, out RaycastHit hit))
            {
                if (hit.transform.root.gameObject == _player.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private abstract class State
    {
        public abstract void OnEnter(EnemyWithStates enemy);
        public abstract void Tick(EnemyWithStates enemy);
        public abstract void OnExit(EnemyWithStates enemy);
    }

    private class PatrolState : State
    {
        public override void OnEnter(EnemyWithStates enemy)
        {
            Debug.Log("Entering Patrol State");
            enemy._navMeshAgent.SetDestination(enemy.PickRandomPatrolPoint());
        }

        public override void Tick(EnemyWithStates enemy)
        {
            if(!enemy._navMeshAgent.hasPath) 
                enemy._navMeshAgent.SetDestination(enemy.PickRandomPatrolPoint());
            
            if(enemy.IsPlayerNoticed()) enemy.SwitchState(new ChaseState());
        }

        public override void OnExit(EnemyWithStates enemy)
        {
            enemy._navMeshAgent.ResetPath();
        }
    }

    private class ChaseState : State
    {
        public override void OnEnter(EnemyWithStates enemy)
        {
            Debug.Log("Entering Chase State");
        }

        public override void Tick(EnemyWithStates enemy)
        {
           enemy._navMeshAgent.destination = enemy._player.transform.position;
           if(!enemy.IsPlayerNoticed()) enemy.SwitchState(new PatrolState());
           else if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) <= enemy.attackDistance)
               enemy.SwitchState(new AttackState());
        }

        public override void OnExit(EnemyWithStates enemy)
        {
            enemy._navMeshAgent.ResetPath();
        }
    }

    private class AttackState : State
    {
        public override void OnEnter(EnemyWithStates enemy)
        {
            Debug.Log("Entering Attack State");
        }

        public override void Tick(EnemyWithStates enemy)
        {
            if(enemy._controller.isAttacking) return;
            if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) <= enemy.attackDistance) 
                enemy._controller.ShowMelee();
            else enemy.SwitchState(new ChaseState());
        }

        public override void OnExit(EnemyWithStates enemy)
        { }
    }
    
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent), typeof(Health))]
public class EnemyWithStates : MonoBehaviour
{
    public static int enemyCount;
    [SerializeField] private float attackDistance;
    public List<Transform> patrolPoints;
    
    public float viewAngle;
    public float damage = 30;

    private PlayerCharacter _player;
    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;
    private Health _myHealth;

    private State _currentState;

    private void Awake()
    {
        _myHealth = GetComponent<Health>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerCharacter>(true);
        enemyCount++;
    }

    private void OnEnable()
    {
        _currentState = new PatrolState();
    }

    private void OnDestroy()
    {
        _myHealth.ZeroHealth -= DieDieMyDarling;
        enemyCount -= 1;
    }
    
    private void DieDieMyDarling()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        _currentState.Tick(this);
    }

    private void SwitchState(State to)
    {
        _currentState.OnExit(this);
        _currentState = to;
        _currentState.OnEnter(this);
    }


    private abstract class State
    {
        public abstract void OnEnter(EnemyWithStates enemy);
        public abstract void Tick(EnemyWithStates enemy);
        public abstract void OnExit(EnemyWithStates enemy);
    }

    private class FollowState : State
    {
        public override void OnEnter(EnemyWithStates enemy)
        {
            Debug.Log("Entering Follow State");
        }

        public override void Tick(EnemyWithStates enemy)
        {
            enemy._navMeshAgent.destination =  enemy._player.transform.position;
            if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) <= enemy.attackDistance)
            {
                enemy.SwitchState( new AttackState());
            }
        }

        public override void OnExit(EnemyWithStates enemy)
        {
            enemy._navMeshAgent.ResetPath();
        }
    }

    private class PatrolState : State
    {
        public override void OnEnter(EnemyWithStates enemy)
        {
            Debug.Log("Entering Patrol State");
            enemy._navMeshAgent.SetDestination(PickNewPatrolPoint(enemy));
        }

        public override void Tick(EnemyWithStates enemy)
        {
            if(!enemy._navMeshAgent.hasPath) 
                enemy._navMeshAgent.SetDestination(PickNewPatrolPoint(enemy));
            
            var direction = enemy._player.transform.position -  enemy.transform.position;
            if (Vector3.Angle( enemy.transform.forward, direction) <  enemy.viewAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast( enemy.transform.position + Vector3.up, direction, out hit))
                {
                    if (hit.collider.gameObject == enemy._player.gameObject)
                    {
                        enemy.SwitchState(new FollowState());
                    }
                }
            }
        }

        public override void OnExit(EnemyWithStates enemy)
        {
            enemy._navMeshAgent.ResetPath();
        }
        
        private Vector3 PickNewPatrolPoint(EnemyWithStates enemy)
        {
            return  enemy.patrolPoints[Random.Range(0, enemy.patrolPoints.Count)].position;
        }
    }

    private class AttackState : State
    {
        private HumanoidController _controller;
        
        public override void OnEnter(EnemyWithStates enemy)
        {
            Debug.Log("Entering Attack State");
            _controller = enemy.GetComponent<HumanoidController>();
        }

        public override void Tick(EnemyWithStates enemy)
        {
            if(_controller.isAttacking) return;
            if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) <= enemy.attackDistance)
                _controller.ShowMelee();
            else
                enemy.SwitchState(new PatrolState());
        }

        public override void OnExit(EnemyWithStates enemy) { }
    }
}



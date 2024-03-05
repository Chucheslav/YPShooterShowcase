using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public PlayerCharacter player;
    public float viewAngle;
    public float damage = 30;

    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;
    private Health _myHealth;
    

    void Start()
    {
        InitComponentLinks();
        PickNewPatrolPoint();
    }
    private void InitComponentLinks()
    {
        _myHealth = GetComponent<Health>();
        _myHealth.ZeroHealth += DieDieMyDarling;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDestroy()
    {
        _myHealth.ZeroHealth -= DieDieMyDarling;
    }

    private void DieDieMyDarling()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!player) return;
        NoticePlayerUpdate();
        ChaseUpdate();
        AttackUpdate();
        PatrolUpdate();
    }

    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;
        _isPlayerNoticed = false;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }

    }
    private void PatrolUpdate()
    {
        if (!_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                PickNewPatrolPoint();
            }
        }
    }
    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }

    private void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                player.TakeDamage(damage * Time.deltaTime);
            }
        }
    }
}
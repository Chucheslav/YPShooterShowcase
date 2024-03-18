using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HumanoidController), typeof(NavMeshAgent))]
public class HCMoveFromNavAgent : MonoBehaviour
{
    private HumanoidController _controller;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _controller = GetComponent<HumanoidController>();
        _agent = GetComponent<NavMeshAgent>();

        _controller.maxSpeedReference = _agent.speed;

    }

    private void Update()
    {
        _controller.moveVector = _agent.velocity;
    }
}
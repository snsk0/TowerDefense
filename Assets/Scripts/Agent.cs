using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Runtime.Enemy.Component;

public class Agent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private LineRenderer lrenderer;
    [SerializeField] private NavMeshObstacle obstacle;
    [SerializeField] private EnemyMove move;
    [SerializeField] private EnemyLook look;
    private bool ismove = true;


    private void Start()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.SetDestination(target.position);
    }
    private void Update()
    {
        if (agent.pathPending == true) return;

        agent.SetDestination(target.position);

        //agentÀ•W‚ğˆê’è‹——£‚É•â³‚·‚é(ˆê’è‚É•Û‚Â‚ÆŒã‚ëŒü‚«‚ÉˆÚ“®‚µ‚È‚¢)
        //‹““®’²® agent‚Æ‚Ì‹——£‚ğˆê’è‚É‚·‚é(maxAgentDistance‚ÉŒÅ’è‚·‚é‚©‚Ç‚¤‚©
        Vector3 direction = agent.nextPosition - transform.position;
        direction = direction.normalized * 1.0f;
        agent.nextPosition = transform.position + direction;


        //‹——£‚Ì”»’è(agent)‚ÆA’Ç]‹——£‚Ì”»’è
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.enabled = false;
            ismove = false;
            obstacle.enabled = true;
            this.enabled = false;
        }
        else
        {
            //ˆÚ“®‚Æ‰ñ“]“ü—Í
            lrenderer.SetPositions(agent.path.corners);
            move.MoveByWorldDir(agent.nextPosition - transform.position);
        }
    }
}

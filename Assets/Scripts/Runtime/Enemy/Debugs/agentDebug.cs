using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Enemy.Debugs
{
    public class agentDebug : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform target;

        private void Start()
        {
            agent.SetDestination(target.position);
        }
    }
}


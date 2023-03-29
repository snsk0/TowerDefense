using UnityEngine;
using UnityEngine.AI;


namespace Runtime.Enemy.Debugs
{
    public class agentLocDebug : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        private void Update()
        {
            gameObject.transform.position = agent.nextPosition;
        }
    }
}

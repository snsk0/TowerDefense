using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using UniRx;

using Enemy.Parameter;
using System;

namespace Enemy
{
    public class EnemyController : MonoBehaviour, IEnemyEventSender
    {
        //ÉpÉâÉÅÅ[É^
        [SerializeField] protected EnemyParameter parameter;

        //Navmesh
        [SerializeField] protected NavMeshAgent agent;



        private Subject<GameObject> _onMove = new Subject<GameObject>();
        private Subject<GameObject> _onAttack = new Subject<GameObject>();
        private Subject<GameObject> _onWait = new Subject<GameObject>();


        public IObservable<GameObject> onMove => _onMove;

        public IObservable<GameObject> onAttack => _onAttack;

        public IObservable<GameObject> onWait => _onWait;



        private bool moved = false;
        public virtual void MoveToTarget()
        {
            agent.SetDestination(parameter.hate.target.transform.position);
            _onMove.OnNext(gameObject);
            Debug.Log("movetoTarget");
            moved = true;
        }

        public virtual void Attack()
        {

        }


        private void Update()
        {
            if (moved) moved = false;
            else agent.SetDestination(transform.position);
        }
    }
}
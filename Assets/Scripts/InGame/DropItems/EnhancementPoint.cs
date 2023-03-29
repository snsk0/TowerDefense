using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using System;

namespace InGame.DropItems
{
    public class EnhancementPoint : MonoBehaviour
    {
        [SerializeField] private SphereCollider pickTrigger;
        [SerializeField] private Rigidbody rigidbody;

        public int point { get; private set; }

        private ISubject<int> pickedSubject = new Subject<int>();
        public IObservable<int> PickedObservable => pickedSubject;

        private void Start()
        {
            pickTrigger.OnTriggerEnterAsObservable()
                .Where(other => other.CompareTag("Player"))
                .Subscribe(_ =>
                {
                    Picked();
                })
                .AddTo(this);
        }

        public void Init(int point)
        {
            this.point = point;

            pickedSubject = new Subject<int>();
        }

        public void Picked()
        {
            pickedSubject.OnNext(point);
            pickedSubject.OnCompleted();
        }

        public void MoveToPlayer(Transform target, Action closedPlayerAction)
        {
            rigidbody.useGravity = false;

            this.FixedUpdateAsObservable()
                .TakeWhile(_=>Vector3.Distance(target.position, transform.position) > 0.5f)
                .Subscribe(_ =>
                {
                    var dir = (target.position - (transform.position + Vector3.up)).normalized;
                    rigidbody.AddForce(dir * 100);
                },
                ()=>
                {
                    closedPlayerAction?.Invoke();
                    rigidbody.useGravity = true;
                })
                .AddTo(this);
        }
    }
}


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
            SetFirstImpulse();
        }

        private void SetFirstImpulse()
        {
            var horizontalVec = Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0)) * new Vector3(0.25f, 0, 0);
            var impalse = (horizontalVec + Vector3.up)*5;
            rigidbody.AddForce(impalse, ForceMode.Impulse);
        }

        private void Picked()
        {
            pickedSubject.OnNext(point);
            pickedSubject.OnCompleted();
        }

        public void MoveToPlayer(Transform target, Action closedPlayerAction)
        {
            rigidbody.useGravity = false;
            Func<Vector3> targetPosition = () => target.position + Vector3.up;

            this.FixedUpdateAsObservable()
                .TakeWhile(_=>Vector3.Distance(targetPosition(), transform.position) > 0.5f)
                .Subscribe(_ =>
                {
                    var dir = (targetPosition() - transform.position).normalized;
                    rigidbody.AddForce(dir * 10);
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


using System;
using UnityEngine;

namespace Enemy
{
    public interface IEnemyEventSender
    {
        public IObservable<GameObject> onMove { get; }
        public IObservable<GameObject> onAttack { get; }
    }
}
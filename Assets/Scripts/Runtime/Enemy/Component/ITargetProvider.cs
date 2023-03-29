using UnityEngine;

using UniRx;


namespace Runtime.Enemy.Component
{
    public interface ITargetProvider
    {
        public IReadOnlyReactiveProperty<GameObject> target { get; }
    }
}

using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerAttacker : MonoBehaviour
    {
        protected PlayerParameter playerParameter;
        protected bool usableSpecial = true;

        private readonly ISubject<float> remainingTimeSubject = new Subject<float>();
        public IObservable<float> RemainingTimeObservable => remainingTimeSubject;

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        protected async UniTaskVoid StartCoolTimeCount(CancellationToken token)
        {
            usableSpecial = false;
            var time = playerParameter.SpecialAttackCoolTime;
            while (true)
            {
                await UniTask.DelayFrame(1, cancellationToken: token);
                time -= Time.deltaTime;
                remainingTimeSubject.OnNext(time);
                if (time <= 0)
                    break;
            }
            usableSpecial = true;
        }
    }
}


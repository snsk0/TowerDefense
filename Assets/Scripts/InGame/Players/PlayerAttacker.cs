using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerAttacker : MonoBehaviour
    {
        protected PlayerParameter playerParameter;
        protected bool usableSpecial = true;

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        protected async UniTaskVoid StartCoolTimeCount(CancellationToken token)
        {
            usableSpecial = false;
            var count = playerParameter.SpecialAttackCoolTime;
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
                count--;
                if (count <= 0)
                    break;
            }
            usableSpecial = true;
        }
    }
}


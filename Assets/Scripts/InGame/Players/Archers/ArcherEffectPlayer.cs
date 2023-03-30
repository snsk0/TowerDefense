using Cysharp.Threading.Tasks;
using InGame.Targets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players.Archers
{
    public class ArcherEffectPlayer : PlayerEffectPlayer
    {
        [SerializeField] private ParticleSystem attackEffect;

        private TargetManager targetManager;

        public void Init(TargetManager targetManager)
        {
            this.targetManager = targetManager;
        }

        public async UniTaskVoid PlayNormalAttackEffect(CancellationToken token)
        {
            Func<Vector3> start = () => transform.position+Vector3.up;
            Func<Vector3> end = () => targetManager.TargetedTransform.Value.position + Vector3.up;

            float elaspedTime = 0f;
            attackEffect.Play();

            while (true)
            {
                var position = Vector3.Lerp(start(), end(), elaspedTime);
                attackEffect.transform.position = position;
                await UniTask.DelayFrame(1, cancellationToken: token);

                if (elaspedTime >= 1)
                    break;

                elaspedTime += Time.deltaTime;
            }
        }
    }
}


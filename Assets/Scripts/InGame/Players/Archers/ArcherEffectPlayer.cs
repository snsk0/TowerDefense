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
        [SerializeField] private ParticleSystem normalAttackEffect;
        [SerializeField] private ParticleSystem specialAttackEffect;

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
            normalAttackEffect.Play();

            while (true)
            {
                var position = Vector3.Lerp(start(), end(), elaspedTime * (10 / 3));
                normalAttackEffect.transform.position = position;
                await UniTask.DelayFrame(1, cancellationToken: token);

                if (elaspedTime >= 0.3f)
                    break;

                elaspedTime += Time.deltaTime;
            }
        }

        public async UniTaskVoid PlaySpecialAttackEffect(CancellationToken token)
        {
            Func<Vector3> start = () => transform.position + Vector3.up;
            Func<Vector3> end = () => targetManager.TargetedTransform.Value.position + Vector3.up;

            float elaspedTime = 0f;
            specialAttackEffect.Play();

            while (true)
            {
                var position = Vector3.Lerp(start(), end(), elaspedTime * (10 / 3));
                specialAttackEffect.transform.position = position;
                await UniTask.DelayFrame(1, cancellationToken: token);

                if (elaspedTime >= 0.3f)
                    break;

                elaspedTime += Time.deltaTime;
            }

            //Func<Vector3> start = () => transform.position + Vector3.up;
            //Func<Vector3> end = () => targetManager.TargetedTransform.Value.position + Vector3.up;

            //Vector3[] middlePoint = new Vector3[5];
            //for(var i = 0; i < 5; i++)
            //{
            //    var vec = (Quaternion.Euler(transform.right) * (end() - start())).normalized;
            //    middlePoint[i] = ((end() - start()) / 2) + (Quaternion.Euler((end() - start()).normalized * UnityEngine.Random.Range(-90f, 90f)) * (vec*0.1f));
            //    specialAttackEffects[i].Play();
            //}

            //float elaspedTime = 0f;


            //while (true)
            //{
            //    for(var i = 0; i < 5; i++)
            //    {
            //        var pos1= Vector3.Lerp(start(), middlePoint[i], elaspedTime);
            //        var pos2= Vector3.Lerp(middlePoint[i], end(), elaspedTime);
            //        var position = Vector3.Lerp(pos1, pos2, elaspedTime);
            //        specialAttackEffects[i].transform.position = position;
            //    }

            //    await UniTask.DelayFrame(1, cancellationToken: token);

            //    if (elaspedTime >= 1)
            //        break;

            //    elaspedTime += Time.deltaTime;
            //}
        }
    }
}


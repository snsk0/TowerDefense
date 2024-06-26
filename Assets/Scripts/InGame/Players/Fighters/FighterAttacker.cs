using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using InGame.Damages;
using InGame.Enemies;
using Runtime.Enemy;

namespace InGame.Players.Fighters
{
    public class FighterAttacker : PlayerAttacker
    {
        [SerializeField] private FighterAnimationPlayer fighterAnimationPlayer;
        [SerializeField] private FighterAttackCollider fighterNormalAttackCollider;
        [SerializeField] private FighterAttackCollider fighterSpecialAttackCollider;

        private void Start()
        {
            fighterNormalAttackCollider.OnTriggerEnterAsObservable()
                .Select(trigger => trigger.GetComponent<IDamagable>())
                .Where(enemy => enemy != null)
                .Subscribe(enemy =>
                {
                    var attackValue = playerParameter.GetCalculatedValue(PlayerParameterType.AttackValue);
                    enemy.Damage(attackValue, 1, 1, gameObject);
                })
                .AddTo(this);


            fighterSpecialAttackCollider.OnTriggerEnterAsObservable()
                .Select(trigger => trigger.GetComponent<IDamagable>())
                .Where(enemy => enemy != null)
                .Subscribe(enemy =>
                {
                    var attackValue = playerParameter.GetCalculatedValue(PlayerParameterType.AttackValue) * 3f;
                    enemy.Damage(attackValue, 1, 3, gameObject);
                })
                .AddTo(this);
        }

        public void NormalAttack()
        {
            fighterAnimationPlayer.PlayNormalAttackAniamtion(fighterNormalAttackCollider.EnableCollider);
        }

        public async UniTask SpecialAttack()
        {
            if (fighterAnimationPlayer.currentAttackState!=PlayerAttackStateType.None)
                return;

            if (!usableSpecial)
                return;

            StartCoolTimeCount(this.GetCancellationTokenOnDestroy()).Forget();
            await fighterAnimationPlayer.PlaySpecialAttackAnimation(fighterSpecialAttackCollider.EnableCollider, this.GetCancellationTokenOnDestroy());
        }
    }
}
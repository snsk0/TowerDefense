using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Fighters
{
    public class FighterAttacker : PlayerAttacker
    {
        [SerializeField] private PlayerAnimationPlayer playerAnimationPlayer;
        [SerializeField] private FighterAttackCollider fighterAttackCollider;

        public override void Attack()
        {
            playerAnimationPlayer.PlayAttackAnimation(this.GetCancellationTokenOnDestroy(), fighterAttackCollider.EnableCollider).Forget();
        }
    }
}
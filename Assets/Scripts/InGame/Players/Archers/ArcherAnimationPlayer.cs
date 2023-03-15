using InGame.Players.Animators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Archers
{
    public class ArcherAnimationPlayer : PlayerAnimationPlayer
    {
        public void PlayChargeAnimation()
        {
            animator.SetBool(AnimatorParameterHashes.Charging, true);
        }

        public void PlayReleaseAnimation()
        {
            animator.SetBool(AnimatorParameterHashes.Charging, false);
        }
    }
}


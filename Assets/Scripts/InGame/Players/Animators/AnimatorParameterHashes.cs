using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Animators
{
    public static class AnimatorParameterHashes
    {
        public static int FirstAttack => Animator.StringToHash("FirstAttack");
        public static int SecondAttack => Animator.StringToHash("SecondAttack");
        public static int ThirdAttack => Animator.StringToHash("ThirdAttack");
        public static int Attack => Animator.StringToHash("Attack");
        public static int Jump => Animator.StringToHash("Jump");
        public static int Run => Animator.StringToHash("Run");
        public static int Charging => Animator.StringToHash("Charging");
        public static int NormalAttackInterval => Animator.StringToHash("NormalAttackInterval");
    }
}


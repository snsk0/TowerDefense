using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Animators
{
    public static class AnimatorParameterHashes
    {
        //public static int ThirdAttack => Animator.StringToHash("ThirdAttack");
        public static int Attack => Animator.StringToHash("Attack");
        public static int NormalAttack => Animator.StringToHash("NormalAttack");
        public static int Jump => Animator.StringToHash("Jump");
        public static int Run => Animator.StringToHash("Run");
        public static int NormalAttackInterval => Animator.StringToHash("NormalAttackInterval");

        //ファイター用
        public static int FirstNormalAttackSpeed => Animator.StringToHash("FirstNormalAttackSpeed");
        public static int SecondNormalAttackSpeed => Animator.StringToHash("SecondNormalAttackSpeed");
    }
}


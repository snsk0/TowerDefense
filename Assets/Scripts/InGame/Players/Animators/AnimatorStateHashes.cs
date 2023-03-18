using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Animators
{
    public static class AnimatorStateHashes
    {
        //タグのハッシュ
        public static int Attack => Animator.StringToHash("Attack");
        public static int Jump => Animator.StringToHash("Jump");
        public static int Idle => Animator.StringToHash("Idle");
        public static int Run => Animator.StringToHash("Run");
        public static int GetUp => Animator.StringToHash("GetUp");
        public static int Damaged => Animator.StringToHash("Damaged");

        //名前のハッシュ
        public static int FirstNormalAttack => Animator.StringToHash("FirstNormalAttack");
        public static int SecondNormalAttack => Animator.StringToHash("SecondNormalAttack");
        //public static int ThirdAttack => Animator.StringToHash("ThirdAttack");
    }
}


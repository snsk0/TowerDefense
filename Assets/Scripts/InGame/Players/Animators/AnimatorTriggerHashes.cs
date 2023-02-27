using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Animators
{
    public static class AnimatorTriggerHashes
    {
        public static int FirstAttack => Animator.StringToHash("FirstAttack");
        public static int SecondAttack => Animator.StringToHash("SecondAttack");
        public static int ThirdAttack => Animator.StringToHash("ThirdAttack");
        public static int Jump => Animator.StringToHash("Jump");
    }
}


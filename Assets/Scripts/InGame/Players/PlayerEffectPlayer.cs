using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerEffectPlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem smallHitEffect;

        public void PlaySmallHitEffect()
        {
            smallHitEffect.Play();
        }

        public void PlayNormalHitEffect()
        {
            smallHitEffect.Play();
        }

        public void PlayHugeHitEffect()
        {
            smallHitEffect.Play();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Fighters
{
    public class FighterEffectPlayer : PlayerEffectPlayer
    {
        [SerializeField] private ParticleSystem firstSlashEffect;
        [SerializeField] private ParticleSystem secondSlashEffect;

        public void PlayNormalAttackEffect(bool isFirst)
        {
            if (isFirst)
            {
                firstSlashEffect.Play();
            }
            else
            {
                secondSlashEffect.Play();
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Damages
{
    public class Damage
    {
        public float attackValue { get; private set; } 
        public KnockbackType knockbackType { get; private set; }

        public Damage(float attackValue, KnockbackType knockbackType)
        {
            this.attackValue = attackValue;
            this.knockbackType = knockbackType;
        }
    }
}


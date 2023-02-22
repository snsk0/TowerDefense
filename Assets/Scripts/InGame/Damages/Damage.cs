using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Damages
{
    public class Damage
    {
        public float attackValue { get; private set; } 

        public Damage(float attackValue)
        {
            this.attackValue = attackValue;
        }
    }
}


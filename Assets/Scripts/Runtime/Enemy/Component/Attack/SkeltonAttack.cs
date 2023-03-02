using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Enemy.Component.Attack
{
    public class SkeltonAttack : EnemyAttack
    {
        [SerializeField] private GameObject attacker;

        public override float AttackToTarget(GameObject target, int index)
        {
            attacker.transform.LookAt(target.transform);
            return 1;
        }

    }
}

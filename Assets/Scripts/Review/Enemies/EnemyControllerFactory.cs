using Review.Enemies.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies
{
    public class EnemyControllerFactory
    {
        public EnemyController CreateEnemyController(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.None:
                    return null;
                case EnemyType.Slime:
                    return new SlimeController();
                case EnemyType.Goblin:
                    return new GoblinController();
                case EnemyType.Golem:
                    return new GolemController();
                default:
                    return null;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies
{
    public class EnemyManager
    {
        private EnemyGenerator enemyGenerator;
        private EnemyControllerFactory enemyControllerFactory;

        public void GenerateEnemy()
        {
            enemyGenerator.GenerateEnemy(EnemyType.None);
            enemyControllerFactory.CreateEnemyController(EnemyType.None);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies
{
    public class EnemyManager
    {
        private EnemyGenerator enemyGenerator;
        private EnemyControllerFactory enemyControllerFactory;

        public EnemyManager(EnemyGenerator enemyGenerator, EnemyControllerFactory enemyControllerFactory)
        {
            this.enemyGenerator = enemyGenerator;
            this.enemyControllerFactory = enemyControllerFactory;
        }

        public void GenerateEnemy(EnemyType generateEnemyType)
        {
            var enemy=enemyGenerator.GenerateEnemy(generateEnemyType);
            enemyControllerFactory.CreateEnemyController(generateEnemyType, enemy);
        }
    }
}


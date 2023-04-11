using Review.Enemies.Controllers;
using Review.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Review.Enemies
{
    public class EnemyControllerFactory
    {
        private readonly StateMachineFactory stateMachineFactory;

        [Inject]
        public EnemyControllerFactory(StateMachineFactory stateMachineFactory)
        {
            this.stateMachineFactory = stateMachineFactory;
        }

        public EnemyController CreateEnemyController(EnemyType enemyType, GameObject targetObject)
        {
            switch (enemyType)
            {
                case EnemyType.None:
                    return null;
                case EnemyType.Slime:
                    return new SlimeController(targetObject, stateMachineFactory);
                case EnemyType.Goblin:
                    return new GoblinController(targetObject, stateMachineFactory);
                case EnemyType.Golem:
                    return new GolemController(targetObject, stateMachineFactory);
                default:
                    return null;
            }
        }
    }
}


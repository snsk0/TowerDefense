using Review.Enemies.Controllers;
using Review.StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Review.Enemies
{
    public class EnemyControllerFactory : StateMachineControllerFactory
    {
        [Inject]
        public EnemyControllerFactory(StateMachineFactory stateMachineFactory) : base(stateMachineFactory)
        {
            
        }

        public EnemyController CreateEnemyController(EnemyType enemyType, GameObject targetObject)
        {
            switch (enemyType)
            {
                case EnemyType.None:
                    return null;
                case EnemyType.Slime:
                    return (SlimeController)CreateStateMachineController(typeof(SlimeController), targetObject);
                case EnemyType.Goblin:
                    return (GoblinController)CreateStateMachineController(typeof(GoblinController), targetObject);
                case EnemyType.Golem:
                    return (GolemController)CreateStateMachineController(typeof(GolemController), targetObject);
                default:
                    return null;
            }
        }

        protected override StateMachineController CreateStateMachineController(Type controllerType, GameObject targetObject)
        {
            object[] constructorArgs = new object[] { targetObject, stateMachineFactory };
            return (EnemyController)Activator.CreateInstance(controllerType, constructorArgs);
        }

    }
}


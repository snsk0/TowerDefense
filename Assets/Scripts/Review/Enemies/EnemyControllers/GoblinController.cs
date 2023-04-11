using Review.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies.Controllers
{
    public class GoblinController : EnemyController
    {
        public GoblinController(GameObject targetObject, StateMachineFactory stateMachineFactory) : base(targetObject, stateMachineFactory)
        {
            stateMachineType = StateMachineType.Goblin;
        }
    }
}


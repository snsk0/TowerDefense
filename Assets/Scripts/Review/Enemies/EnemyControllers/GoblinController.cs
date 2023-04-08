using Review.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies.Controllers
{
    public class GoblinController : EnemyController
    {
        public GoblinController()
        {
            stateMachineType = StateMachineType.Goblin;
        }
    }
}


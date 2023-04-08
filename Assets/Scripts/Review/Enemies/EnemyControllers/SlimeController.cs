using Review.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies.Controllers
{
    public class SlimeController : EnemyController
    {
        public SlimeController()
        {
            stateMachineType = StateMachineType.Slime;
        }
    }
}


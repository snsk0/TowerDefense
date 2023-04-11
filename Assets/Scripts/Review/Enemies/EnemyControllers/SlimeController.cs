using Review.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies.Controllers
{
    public class SlimeController : EnemyController
    {
        protected override string settingFilePath { get; set; } = "SlimeStateMachineSetting";

        public SlimeController(GameObject targetObject, StateMachineFactory stateMachineFactory) : base(targetObject, stateMachineFactory)
        {
            stateMachineType = StateMachineType.Slime;
        }
    }
}


using Review.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies
{
    public class EnemyController : StateMachineController
    {
        protected override string settingFilePath { get; set; }

        public EnemyController(GameObject targetObject, StateMachineFactory stateMachineFactory) : base(targetObject, stateMachineFactory)
        {

        }
    }
}


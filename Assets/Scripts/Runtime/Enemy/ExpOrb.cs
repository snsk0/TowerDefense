using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Runtime.Enemy.Parameter;

namespace Runtime.Enemy
{
    public class ExpOrb : MonoBehaviour, IExpProvider
    {
        public float exp { get; private set; }
        public void SetInitialize(EnemyParameter parameter)
        {
            exp = parameter.exp;
        }
    }



    public interface IExpProvider
    {
        public float exp { get; }
    }
}

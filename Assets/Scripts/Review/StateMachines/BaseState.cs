using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Review.StateMachines
{
    [Serializable]
    public class BaseState
    {
        public ResultType result { get; protected set; }

        public bool IsRepeatable { get; protected set; } = false;

        public virtual void Execute(Blackboard blackboard)
        {
            //子クラスで具体的な実装
        }

        public virtual void Abort()
        {
            //子クラスで具体的な実装
        }

        protected virtual void FinishState()
        {
            //子クラスで具体的な実装
        }
    }
}


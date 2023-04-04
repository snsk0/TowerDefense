using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees
{
    public enum AbortType
    {
        None,
        Self,
        LowerPriority,
        Both,
    }
}
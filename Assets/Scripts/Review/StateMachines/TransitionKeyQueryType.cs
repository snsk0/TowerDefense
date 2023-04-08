using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachine
{
    public enum TransitionKeyQueryType
    {
        IsSet=0,
        IsNotSet=1,
        IsTrue=2,
        IsFalse=3,
        IsEqual=4,
        IsNotEqual=5,
        IsLessThan=6,
        IsLessThanOrEqual=7,
        IsGreaterThan=8,
        IsGreaterThanOrEqual=9,
    }
}

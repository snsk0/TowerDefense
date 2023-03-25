using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public enum PlayerBaseStateType
    {
        None,
        Idle,
        Move,
        Jump,
        Sprint
    }

    public enum PlayerAttackStateType
    {
        None,
        Normal,
        Special
    }
}


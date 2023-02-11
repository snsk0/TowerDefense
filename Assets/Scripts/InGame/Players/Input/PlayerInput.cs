using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Input
{
    public class PlayerInput
    {
        public Vector3 MoveVec => new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical")).normalized;
        public bool HadPushedJump => UnityEngine.Input.GetKeyDown(KeyCode.Space);
        public bool HadPushedAttack => UnityEngine.Input.GetMouseButtonDown(0);
        public bool HadPushedAvoid => UnityEngine.Input.GetKeyDown(KeyCode.LeftShift);
    }
}


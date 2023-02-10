using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Input
{
    public class PlayerInput
    {
        public Vector3 MoveVec => new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical")).normalized;
        public bool HadPushedJump => UnityEngine.Input.GetKey(KeyCode.Space);
        public bool HadPushedAttack => UnityEngine.Input.GetMouseButtonDown(0);
    }
}


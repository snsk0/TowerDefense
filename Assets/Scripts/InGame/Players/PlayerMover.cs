using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float moveSpeed = 1f;

        public void Move(Vector3 moveVec)
        {
            rigidbody.velocity = moveVec * moveSpeed;
        }
    }
}


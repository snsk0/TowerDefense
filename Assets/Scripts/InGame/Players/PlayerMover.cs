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
            if (rigidbody == null)
                return;

            var velocity = moveVec * moveSpeed + new Vector3(0, rigidbody.velocity.y, 0);
            rigidbody.velocity = velocity;
        }
    }
}


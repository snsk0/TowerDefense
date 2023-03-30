using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Runtime.Enemy.Component;

namespace Runtime.Enemy.Debugs {
    public class Damager : MonoBehaviour
    {
        [SerializeField] private EnemyController controller;


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                controller.Damage(3, Vector3.right, 0, gameObject);
                Debug.Log("D");
            }
        }
    }
}

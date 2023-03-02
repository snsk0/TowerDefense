using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Runtime.Enemy.Debug
{
    public class AddDamager : MonoBehaviour
    {
        [SerializeField] private EnemyController controller;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                controller.Damage(5, 0.1f, 0, gameObject);
            }
        }
    }

}
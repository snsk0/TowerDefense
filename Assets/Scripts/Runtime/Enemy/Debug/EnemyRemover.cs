using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace Runtime.Enemy.Debug
{
    public class EnemyRemover : MonoBehaviour
    {
        [SerializeField] private EnemyController controller;


        private void Start()
        {
            controller.onDamage.Subscribe(e =>
            {
                if (e.currentHealth <= 0) Destroy(controller.gameObject);
            }).AddTo(this);
        }
    }
}

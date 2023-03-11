using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace InGame.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public int currentHP { get; private set; } = 2;

        private readonly ReactiveProperty<bool> hadDeadReactiveProperty = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> HadDeadReactiveProperty => hadDeadReactiveProperty;

        public void AddDamage(int damage)
        {
            currentHP -= damage;
            Debug.Log($"EnemyHealth{currentHP}");

            if (currentHP <= 0)
            {
                hadDeadReactiveProperty.Value = true;
                Invoke("DestroyObject",1f);
            }
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}


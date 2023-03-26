using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerHealth : MonoBehaviour
    {
        private PlayerParameter playerParameter;

        public int currentHP { get; private set; }

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;

            currentHP = playerParameter.MaxHP;
        }

        public void AddDamage(int damageValue)
        {
            currentHP -= damageValue;
            Debug.Log($"PlayerHealth{currentHP}");
        }

        public void Heal(int value)
        {
            currentHP = Mathf.Clamp(currentHP + value, 0, playerParameter.MaxHP);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationPlayer playerAnimationPlayer;

        private PlayerParameter playerParameter;

        public int currentHP { get; private set; }

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;

            currentHP = Mathf.FloorToInt(playerParameter.GetCalculatedValue(PlayerParameterType.HP));
        }

        public void AddDamage(int damageValue)
        {
            currentHP -= (int)Mathf.Clamp(damageValue - playerParameter.GetCalculatedValue(PlayerParameterType.DefenceValue), 1, Mathf.Infinity);
            
            if (currentHP <= 0)
            {
                currentHP = 0;
                playerAnimationPlayer.PlayDeathAnimation();
            }

            Debug.Log($"PlayerHealth:{currentHP}");
        }

        public void Heal(int value)
        {
            currentHP = Mathf.Clamp(currentHP + value, 0, Mathf.FloorToInt(playerParameter.GetCalculatedValue(PlayerParameterType.HP)));
        }
    }
}


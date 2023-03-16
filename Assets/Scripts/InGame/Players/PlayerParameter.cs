using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerParameter
    {
        public int maxHP { get; private set; } = 100;
        public float baseAttackValue { get; private set; } = 1f;
        public float baseDefenceValue { get; private set; } = 1f;
        public float baseInvincibleTime { get; private set; } = 0.3f;
        public float baseAvoidDistance { get; private set; } = 100f;
        public float baseMoveSpeed { get; private set; } = 1f;
        public float baseAttackInterval { get; private set; } = 1f;

        public float maxHPMagnification { get; private set; } = 1f;
        public float attackMagnification { get; private set; } = 1f;
        public float defenceMagnification { get; private set; } = 1f;
        public float invincibleTimeMagnification { get; private set; } = 1f;
        public float avoidDistanceMagnification { get; private set; } = 1f;
        public float moveSpeedMagnification { get; private set; } = 1f;
        public float AttackIntervalMagnification { get; private set; } = 1f;

        public float addMaxHP { get; private set; } = 0f;
        public float addAttackValue { get; private set; } = 0f;
        public float addDefenceValue { get; private set; } = 0f;
        public float addinvincibleTime { get; private set; } = 0f;
        public float addAvoidDistance { get; private set; } = 0f;
        public float addMoveSpeed { get; private set; } = 0f;

        public float AttackInterval => baseAttackInterval * AttackIntervalMagnification;

        public PlayerParameter(PlayerCharacterType playerCharacterType)
        {

        }

        public void IncreaseAddParameter(PlayerParameterType playerParameterType, int value)
        {
            switch (playerParameterType)
            {
                case PlayerParameterType.HP:
                    addMaxHP += value;
                    break;
                case PlayerParameterType.AttackPower:
                    addAttackValue += value;
                    break;
                case PlayerParameterType.DefencePower:
                    addDefenceValue += value;
                    break;
                case PlayerParameterType.MoveSpeed:
                    addMoveSpeed += value;
                    break;
            }
        }
    }
}


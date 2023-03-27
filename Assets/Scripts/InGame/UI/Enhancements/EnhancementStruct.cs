using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.UI.Enhancements
{
    public enum ValueType
    {
        Base,
        Add,
        Mugnification,
    }

    public struct EnhancementStruct
    {
        public PlayerParameterType playerParameterType { get; private set; }
        public ValueType valueType { get; private set; }
        public float value { get; private set; }
        public int usePoint { get; private set; }

        public EnhancementStruct(PlayerParameterType playerParameterType, ValueType valueType, float value, int usePoint)
        {
            this.playerParameterType = playerParameterType;
            this.valueType = valueType;
            this.value = value;
            this.usePoint = usePoint;
        }
    }
}


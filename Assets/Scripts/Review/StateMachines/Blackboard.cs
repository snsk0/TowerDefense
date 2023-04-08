using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachines
{
    public class Blackboard
    {
        private Dictionary<string, object> valueDic;

        public Blackboard(BlackboardSetting blackboardSetting)
        {
            
        }

        public T GetValue<T>(string key)
        {
            valueDic.TryGetValue(key, out var value);

            if (value == null)
            {
                Debug.LogError("ílÇ™éÊìæÇ≈Ç´Ç‹ÇπÇÒ");
                return default;
            }

            if(value is T)
            {
                return (T)value;
            }

            return default;
        }

        public void SetValue<T>(string key, T value)
        {
            if (valueDic.ContainsKey(key))
            {
                valueDic[key] = value;
            }
            else
            {
                Debug.LogWarning("ÉLÅ[Ç™ëπzì‡ÇµÇ‹ÇπÇÒ");
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Review.StateMachines
{
    public class Blackboard
    {
        private Dictionary<string, object> valueDic;

        public Blackboard(IEnumerable<KeyValuePair<string, object>> valueDic)
        {
            this.valueDic = valueDic.ToDictionary(t=>t.Key, t=>t.Value);
        }

        public T GetValue<T>(string key)
        {
            var value = valueDic.Single(x => x.Key == key).Value;

            if (value == null)
            {
                Debug.LogError("’l‚ªæ“¾‚Å‚«‚Ü‚¹‚ñ");
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
            if (valueDic.Any(x=>x.Key==key))
            {
                valueDic[key] = value;
            }
            else
            {
                Debug.LogWarning("ƒL[‚ª‘¶İ‚µ‚Ü‚¹‚ñ");
            }
        }
    }
}


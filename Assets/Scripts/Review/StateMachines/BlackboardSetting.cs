using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.StateMachines
{
    [Serializable]
    public class BlackboardKeyValuePair
    {
        [SerializeField] private string keyString;
        [SerializeField] private BlackboardValueType valueType;

        public string KeyString => keyString;
        public BlackboardValueType ValueType => valueType;
    }

    [CreateAssetMenu(menuName ="BlackboardSetting", fileName ="BlackboardSetting")]
    public class BlackboardSetting : ScriptableObject
    {
        [SerializeField] private BlackboardKeyValuePair[] blackboardKeyValuePairs;

        public IEnumerable<string> keyStrings => blackboardKeyValuePairs.Select(x => x.KeyString);

        private void OnValidate()
        {
            if (blackboardKeyValuePairs.GroupBy(x => x.KeyString).Any(x => x.Skip(1).Any()))
            {
                Debug.LogWarning("d•¡‚µ‚½ƒL[‚ª‘¶Ý‚µ‚Ü‚·");
            }
        }

        public string GetKeyName(int index)
            => blackboardKeyValuePairs[index].KeyString;

        public BlackboardValueType GetBlackBoardValueType(string key)
            => blackboardKeyValuePairs.Single(x => x.KeyString == key).ValueType;
    }
}


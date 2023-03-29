using System.Collections.Generic;
using UnityEngine;

namespace StateMachines.BlackBoards
{
    public class UnityBlackBoard : MonoBehaviour, IBlackBoard
    {
        //ブラックボードのデータ
        private Dictionary<string, object> valueList;

        //初期化
        private void Awake()
        {
            valueList = new Dictionary<string, object>();
        }


        //なかった場合デフォルト値を返す
        public T GetValue<T>(string name)
        {
            if (valueList.ContainsKey(name))
            {
                object value = valueList[name];
                if (value is T) return (T)value;
            }
            return default(T);
        }


        public bool SetValue<T>(string name, T value)
        {
            if (!valueList.ContainsKey(name))
            {
                valueList.Add(name, value);
            }
            return false;
        }
    }
}

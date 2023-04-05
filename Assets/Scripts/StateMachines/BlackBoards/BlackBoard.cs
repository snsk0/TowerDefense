using System.Collections.Generic;

namespace StateMachines.BlackBoards
{
    public class BlackBoard : IBlackBoard
    {
        //ブラックボードのデータ
        //stringよりEnumがいい
        private readonly Dictionary<string, object> valueList;

        //初期化
        public BlackBoard()
        {
            valueList = new Dictionary<string, object>();
        }


        //なかった場合エラーを返す
        public T GetValue<T>(string name)
        {
            if (valueList.ContainsKey(name))
            {
                object value = valueList[name];
                if (value is T) return (T)value;
            }
            throw new System.Exception("BlackBoardError: " + name);
        }


        public bool SetValue<T>(string name, T value)
        {
            if (!valueList.ContainsKey(name))
            {
                valueList.Add(name, value);
            }
            else
            {
                valueList[name] = value;
            }
            return true;
        }
    }
}
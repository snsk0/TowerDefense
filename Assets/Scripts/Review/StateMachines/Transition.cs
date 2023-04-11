using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Review.StateMachines
{
    [Serializable]
    public class Transition
    {
        //エディタ上で遷移条件を指定する
        [SerializeField] List<TransitionCondition> consitions;

        //エディタ拡張で使用する値
        [SerializeField] private BaseState beforeState;
        [SerializeField] private BaseState afterState;

        public IEnumerable<TransitionCondition> Conditions => consitions;
    }

    [Serializable]
    public class TransitionCondition
    {
        [SerializeField] private int useKey = 0;
        [SerializeField] private TransitionKeyQueryType keyQueryType;
        [SerializeField] private int intValue;
        [SerializeField] private float floatValue;

        [SerializeField] private string keyName;
        [SerializeField] private BlackboardValueType valueType;
        private Blackboard blackboard;

        public bool Condition()
        {
            switch (valueType)
            {
                case BlackboardValueType.Integer:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsEqual:
                            return blackboard.GetValue<int>(keyName) == intValue;
                        case TransitionKeyQueryType.IsNotEqual:
                            return blackboard.GetValue<int>(keyName) != intValue;
                        case TransitionKeyQueryType.IsLessThan:
                            return blackboard.GetValue<int>(keyName) < intValue;
                        case TransitionKeyQueryType.IsLessThanOrEqual:
                            return blackboard.GetValue<int>(keyName) <= intValue;
                        case TransitionKeyQueryType.IsGreaterThan:
                            return blackboard.GetValue<int>(keyName) > intValue;
                        case TransitionKeyQueryType.IsGreaterThanOrEqual:
                            return blackboard.GetValue<int>(keyName) >= intValue;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.Float:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsEqual:
                            return blackboard.GetValue<float>(keyName) == floatValue;
                        case TransitionKeyQueryType.IsNotEqual:
                            return blackboard.GetValue<float>(keyName) != floatValue;
                        case TransitionKeyQueryType.IsLessThan:
                            return blackboard.GetValue<float>(keyName) < floatValue;
                        case TransitionKeyQueryType.IsLessThanOrEqual:
                            return blackboard.GetValue<float>(keyName) <= floatValue;
                        case TransitionKeyQueryType.IsGreaterThan:
                            return blackboard.GetValue<float>(keyName) > floatValue;
                        case TransitionKeyQueryType.IsGreaterThanOrEqual:
                            return blackboard.GetValue<float>(keyName) >= floatValue;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.Boolean:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsTrue:
                            return blackboard.GetValue<bool>(keyName);
                        case TransitionKeyQueryType.IsFalse:
                            return !blackboard.GetValue<bool>(keyName);
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.MonoBehaviour:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsSet:
                            return blackboard.GetValue<MonoBehaviour>(keyName) != null;
                        case TransitionKeyQueryType.IsNotSet:
                            return blackboard.GetValue<MonoBehaviour>(keyName) == null;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.GameObject:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsSet:
                            return blackboard.GetValue<GameObject>(keyName) != null;
                        case TransitionKeyQueryType.IsNotSet:
                            return blackboard.GetValue<GameObject>(keyName) == null;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.Transform:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsSet:
                            return blackboard.GetValue<Transform>(keyName) != null;
                        case TransitionKeyQueryType.IsNotSet:
                            return blackboard.GetValue<Transform>(keyName) == null;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }
    }
}


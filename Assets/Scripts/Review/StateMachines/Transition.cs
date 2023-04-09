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

        //エディタ拡張で使用する値
        [SerializeField] private BlackboardSetting blackboardSetting;

        private BlackboardValueType valueType;
        private Blackboard blackboard;

        public void SetBlackboardSetting(BlackboardSetting blackboardSetting)
        {
            this.blackboardSetting = blackboardSetting;
        }

        public bool Condition()
        {
            switch (valueType)
            {
                case BlackboardValueType.Integer:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsEqual:
                            return blackboard.GetValue<int>(blackboardSetting.GetKeyName(useKey)) == intValue;
                        case TransitionKeyQueryType.IsNotEqual:
                            return blackboard.GetValue<int>(blackboardSetting.GetKeyName(useKey)) != intValue;
                        case TransitionKeyQueryType.IsLessThan:
                            return blackboard.GetValue<int>(blackboardSetting.GetKeyName(useKey)) < intValue;
                        case TransitionKeyQueryType.IsLessThanOrEqual:
                            return blackboard.GetValue<int>(blackboardSetting.GetKeyName(useKey)) <= intValue;
                        case TransitionKeyQueryType.IsGreaterThan:
                            return blackboard.GetValue<int>(blackboardSetting.GetKeyName(useKey)) > intValue;
                        case TransitionKeyQueryType.IsGreaterThanOrEqual:
                            return blackboard.GetValue<int>(blackboardSetting.GetKeyName(useKey)) >= intValue;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.Float:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsEqual:
                            return blackboard.GetValue<float>(blackboardSetting.GetKeyName(useKey)) == floatValue;
                        case TransitionKeyQueryType.IsNotEqual:
                            return blackboard.GetValue<float>(blackboardSetting.GetKeyName(useKey)) != floatValue;
                        case TransitionKeyQueryType.IsLessThan:
                            return blackboard.GetValue<float>(blackboardSetting.GetKeyName(useKey)) < floatValue;
                        case TransitionKeyQueryType.IsLessThanOrEqual:
                            return blackboard.GetValue<float>(blackboardSetting.GetKeyName(useKey)) <= floatValue;
                        case TransitionKeyQueryType.IsGreaterThan:
                            return blackboard.GetValue<float>(blackboardSetting.GetKeyName(useKey)) > floatValue;
                        case TransitionKeyQueryType.IsGreaterThanOrEqual:
                            return blackboard.GetValue<float>(blackboardSetting.GetKeyName(useKey)) >= floatValue;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.Boolean:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsTrue:
                            return blackboard.GetValue<bool>(blackboardSetting.GetKeyName(useKey));
                        case TransitionKeyQueryType.IsFalse:
                            return !blackboard.GetValue<bool>(blackboardSetting.GetKeyName(useKey));
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.MonoBehaviour:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsSet:
                            return blackboard.GetValue<MonoBehaviour>(blackboardSetting.GetKeyName(useKey)) != null;
                        case TransitionKeyQueryType.IsNotSet:
                            return blackboard.GetValue<MonoBehaviour>(blackboardSetting.GetKeyName(useKey)) == null;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.GameObject:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsSet:
                            return blackboard.GetValue<GameObject>(blackboardSetting.GetKeyName(useKey)) != null;
                        case TransitionKeyQueryType.IsNotSet:
                            return blackboard.GetValue<GameObject>(blackboardSetting.GetKeyName(useKey)) == null;
                        default:
                            Debug.LogError("型とクエリが一致しません");
                            break;
                    }
                    break;
                case BlackboardValueType.Transform:
                    switch (keyQueryType)
                    {
                        case TransitionKeyQueryType.IsSet:
                            return blackboard.GetValue<Transform>(blackboardSetting.GetKeyName(useKey)) != null;
                        case TransitionKeyQueryType.IsNotSet:
                            return blackboard.GetValue<Transform>(blackboardSetting.GetKeyName(useKey)) == null;
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


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Review.StateMachines
{
    public abstract class StateMachineController
    {
        protected StateMachineType stateMachineType;

        protected abstract string settingFilePath { get; set; }

        private StateMachine usingStateMachine;

        public GameObject targetObject { get; private set; }

        protected StateMachineController(GameObject targetObject, StateMachineFactory stateMachineFactory)
        {
            this.targetObject = targetObject;

            if(settingFilePath == "")
            {
                Debug.LogWarning("ステートマシンの設定ファイルパスが設定されていません");
                return;
            }

            Addressables.LoadAssetAsync<StateMachineSetting>(settingFilePath).Completed += setting =>
            {
                if (setting.Result == null)
                {
                    Debug.LogError($"ステートマシンの設定ファイルパスが正しくありません\nPath{settingFilePath}");
                    return;
                }
                usingStateMachine=stateMachineFactory.CreateStateMachine(setting.Result);
            };
        }
    }
}


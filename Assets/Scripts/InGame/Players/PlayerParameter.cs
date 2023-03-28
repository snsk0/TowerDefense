using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace InGame.Players
{
    public class PlayerParameter
    {
        public class Parameter
        {
            public PlayerParameterType playerParameterType { get; private set; }
            public float baseValue { get; private set; }
            public float addValue { get; private set; }
            public float magnification { get; private set; }

            public float CalculatedValue => (baseValue + addValue) * magnification;

            public Parameter(PlayerParameterType playerParameterType, float baseValue, float addValue, float magnification)
            {
                this.playerParameterType = playerParameterType;
                this.baseValue = baseValue;
                this.addValue = addValue;
                this.magnification = magnification;
            }

            public void IncreaseBaseValue(float value)
                => baseValue += value;

            public void IncreaseAddValue(float value)
                => addValue += value;

            public void IncreaseMagnification(float value)
                => magnification += value;
        }

        private readonly List<Parameter> parameterList = new();

        private readonly ISubject<PlayerParameterType> changedParameterTypeSubject = new Subject<PlayerParameterType>();
        public IObservable<PlayerParameterType> ChangedParameterTypeObservable => changedParameterTypeSubject;

        //計算済みのパラメータの値を取得
        public float GetCalculatedValue(PlayerParameterType parameterType)
            => parameterList.Single(x => x.playerParameterType == parameterType).CalculatedValue;

        //パラメータクラスの取得
        public Parameter GetParameter(PlayerParameterType parameterType)
            => parameterList.Single(x => x.playerParameterType == parameterType);

        public PlayerParameter(PlayerCharacterType playerCharacterType, Action callback)
        {
            Addressables.LoadAssetAsync<TextAsset>("PlayerParameter").Completed += text =>
            {
                SetFirstParameter(text.Result, playerCharacterType);
                callback?.Invoke();
            };
        }

        //JSONから読み込んだパラメータを反映させる
        private void SetFirstParameter(TextAsset textAsset, PlayerCharacterType playerCharacterType)
        {
            var obj = JObject.Parse(textAsset.text);

            switch (playerCharacterType)
            {
                case PlayerCharacterType.Fighter:
                    foreach (var token in obj["Fighter"])
                    {
                        var type = (PlayerParameterType)Enum.ToObject(typeof(PlayerParameterType), token["ParameterType"].Value<int>());
                        var baseValue = token["BaseValue"].Value<float>();
                        var addValue = token["AddValue"].Value<float>();
                        var magnification = token["Magnification"].Value<float>();
                        var parameter = new Parameter(type, baseValue, addValue, magnification);
                        parameterList.Add(parameter);
                    }
                    break;
                case PlayerCharacterType.Archer:
                    foreach (var token in obj["Archer"])
                    {
                        var type = (PlayerParameterType)Enum.ToObject(typeof(PlayerParameterType), token["ParameterType"].Value<int>());
                        var baseValue = token["BaseValue"].Value<float>();
                        var addValue = token["AddValue"].Value<float>();
                        var magnification = token["Magnification"].Value<float>();
                        var parameter = new Parameter(type, baseValue, addValue, magnification);
                        parameterList.Add(parameter);
                    }
                    break;
            }
        }

        public void IncreaseAddValue(PlayerParameterType playerParameterType, int value)
        {
            parameterList.Single(x => x.playerParameterType == playerParameterType).IncreaseAddValue(value);
            changedParameterTypeSubject.OnNext(playerParameterType);
        }

        public void IncreaseMagnificationValue(PlayerParameterType playerParameterType, float value)
        {
            parameterList.Single(x => x.playerParameterType == playerParameterType).IncreaseMagnification(value);
            changedParameterTypeSubject.OnNext(playerParameterType);
        }
    }
}


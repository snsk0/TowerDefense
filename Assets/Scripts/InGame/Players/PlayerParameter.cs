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

        ////各パラメータの基礎値
        public float baseInvincibleTime { get; private set; } = 0.3f;
        public float baseAvoidDistance { get; private set; } = 100f;
        public float baseMoveSpeed { get; private set; } = 1f;
        public float baseJumpPower { get; private set; } = 1f;
        public float baseNormalAttackInterval { get; private set; } = 1f;
        public float baseSpecialAttackCoolTime { get; private set; } = 8f;

        ////各パラメータにかかる倍率
        public float invincibleTimeMagnification { get; private set; } = 1f;
        public float avoidDistanceMagnification { get; private set; } = 1f;
        public float moveSpeedMagnification { get; private set; } = 1f;
        public float jumpPowerMagnification { get; private set; } = 1f;
        public float normalAttackIntervalMagnification { get; private set; } = 1f;
        public float SpecialAttackCoolTimeMagnification { get; private set; } = 1f;

        ////各パラメータの基礎値に上乗せされる値
        public float addinvincibleTime { get; private set; } = 0f;
        public float addAvoidDistance { get; private set; } = 0f;
        public float addMoveSpeed { get; private set; } = 0f;
        public float addJumpPower { get; private set; } = 0f;

        ////各パラメータ計算後の値
        public float InvicibleTime => (baseInvincibleTime + addinvincibleTime) * invincibleTimeMagnification;   //スプリント時の無敵時間
        public float AvoidDistance => (baseAvoidDistance + addAvoidDistance) * avoidDistanceMagnification;      //スプリントの距離
        public float MoveSpeed => (baseMoveSpeed + addMoveSpeed) * moveSpeedMagnification;                      //移動速度
        public float JumpPower => (baseJumpPower + addJumpPower) * jumpPowerMagnification;                      //ジャンプの勢い
        public float NormalAttackInterval => baseNormalAttackInterval * normalAttackIntervalMagnification;      //通常攻撃のインターバル
        public float SpecialAttackCoolTime => baseSpecialAttackCoolTime * SpecialAttackCoolTimeMagnification;   //特殊攻撃のクールタイム

        //計算済みのパラメータの値を取得
        public float GetCalculatedValue(PlayerParameterType parameterType)
            => parameterList.Single(x => x.playerParameterType == parameterType).CalculatedValue;

        //パラメータクラスの取得
        public Parameter GetParameter(PlayerParameterType parameterType)
            => parameterList.Single(x => x.playerParameterType == parameterType);

        private readonly ISubject<PlayerParameterType> changedParameterTypeSubject = new Subject<PlayerParameterType>();
        public IObservable<PlayerParameterType> ChangedParameterTypeObservable => changedParameterTypeSubject;

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


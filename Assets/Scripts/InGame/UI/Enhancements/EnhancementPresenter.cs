using InGame.Players;
using InGame.Players.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer.Unity;
using VContainer;
using InGame.Enemies;
using System;

namespace InGame.UI.Enhancements
{
    public class EnhancementPresenter : ControllerBase, IStartable, IInitializable
    {
        private readonly PlayerManager playerManager;
        private readonly EnhancementView enhancementView;
        private readonly EnemyManager enemyManager;
        private readonly PlayerBackpack playerBackpack;

        private readonly PlayerInput playerInput = new PlayerInput();

        [Inject]
        public EnhancementPresenter(PlayerManager playerManager, EnhancementView enhancementView, EnemyManager enemyManager, PlayerBackpack playerBackpack)
        {
            this.playerManager = playerManager;
            this.enhancementView = enhancementView;
            this.enemyManager = enemyManager;
            this.playerBackpack = playerBackpack;
        }

        public void Initialize()
        {
            playerManager.GeneratePlayerObservable
                .Subscribe(_ =>
                {
                    //初期パラメータの表示
                    ViewFirstParameter();
                })
                .AddTo(this);
        }

        public void Start()
        {
            //パラメータアップのボタンが押されたときの処理
            enhancementView.parameterUpButtonClickObservable
                .Subscribe(s =>
                {
                    switch (s.valueType)
                    {
                        case ValueType.Base:
                            break;
                        case ValueType.Add:
                            playerManager.playerParameter.IncreaseAddValue(s.playerParameterType, (int)s.value);
                            break;
                        case ValueType.Mugnification:
                            playerManager.playerParameter.IncreaseMagnificationValue(s.playerParameterType, s.value);
                            break;
                    }
                    playerBackpack.DecreaseEnhancementPoint(s.usePoint);
                })
                .AddTo(this);

            //強化画面を開いたときの処理
            playerInput.ObserveEveryValueChanged(x => x.HadPushedEnhance)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    enhancementView.ViewPanel();
                })
                .AddTo(this);

            //敵から強化ポイントを取得したときの処理
            enemyManager.DropedEnhancementPointObservable
                .Subscribe(value =>
                {
                    playerBackpack.AddEnhancementPoint(value);
                })
                .AddTo(this);

            //現在の強化ポイントを表示
            playerBackpack.ObserveEveryValueChanged(x => x.enhancementPoint)
                .Subscribe(point =>
                {
                    enhancementView.SetPointText(point);
                    enhancementView.SetInteractableButtons(point);
                })
                .AddTo(this);

            //パラメータが変化したとき表示を更新する
            playerManager.playerParameter.ChangedParameterTypeObservable
                .Subscribe(type =>
                {
                    SetParameterValue(type);
                })
                .AddTo(this);
        }

        private void ViewFirstParameter()
        {
            foreach (var type in Enum.GetValues(typeof(PlayerParameterType)))
            {
                SetParameterValue((PlayerParameterType)type);
            }
        }

        private void SetParameterValue(PlayerParameterType playerParameterType)
        {
            //パラメータに適した値を取得してViewに渡す
            int value = Mathf.FloorToInt(playerManager.playerParameter.GetCalculatedValue(playerParameterType));
            float magnification = playerManager.playerParameter.GetParameter(playerParameterType).magnification;
            enhancementView.SetParameterValue(playerParameterType, value, magnification);
        }
    }
}


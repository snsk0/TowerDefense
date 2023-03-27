using InGame.Players;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Enhancements
{
    public class EnhancementView : MonoBehaviour
    {
        [Serializable]
        public class EnhancementUIElements
        {
            [Serializable]
            public class EnhancementButton
            {
                [SerializeField] Button button;
                [SerializeField] ValueType valueType;
                [SerializeField, Tooltip("強化される値")] float enhancementValue;
                [SerializeField, Tooltip("使用するポイント")] int usePoint;

                public Button Button => button;
                public ValueType ValueType => valueType;
                public float EnhancementValue => enhancementValue;
                public int UsePoint => usePoint;
            }

            [SerializeField] private PlayerParameterType playerParameterType;
            [SerializeField] private TMP_Text valueText;
            [SerializeField] private EnhancementButton[] enhancementButtonArray;

            public PlayerParameterType PlayerParameterType => playerParameterType;
            public TMP_Text ValueText => valueText;
            public EnhancementButton[] EnhancementButtonArray=>enhancementButtonArray;

            public void SetValueText(string text)
            {
                valueText.text = text;
            }
        }

        [SerializeField] private EnhancementUIElements[] enhansementUIElementsList;

        [SerializeField] private GameObject enhancementPanel;
        [SerializeField] private GameObject baseParameterPanel;
        [SerializeField] private GameObject attackParameterPanel;
        [SerializeField] private GameObject moveParameterPanel;
        [SerializeField] private Button basePanelButton;
        [SerializeField] private Button attackPanelButton;
        [SerializeField] private Button movePanelButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private TMP_Text[] pointTextArray;

        private readonly ISubject<EnhancementStruct> parameterUpButtonClickSubject = new Subject<EnhancementStruct>();
        public IObservable<EnhancementStruct> parameterUpButtonClickObservable => parameterUpButtonClickSubject;

        private readonly ISubject<bool> viewPanelSubject = new Subject<bool>();
        public IObservable<bool> ViewPanelObservable => viewPanelSubject;

        private GameObject currentOpenedPanel;

        private void Start()
        {
            //それぞれのボタンが押されたときに強化する値や消費ポイントを送る
            foreach(var UIs in enhansementUIElementsList)
            {
                foreach(var button in UIs.EnhancementButtonArray)
                {
                    button.Button.onClick.AddListener(() => parameterUpButtonClickSubject.OnNext(new EnhancementStruct(UIs.PlayerParameterType, button.ValueType, button.EnhancementValue, button.UsePoint)));
                }
            }

            basePanelButton.onClick.AddListener(() => ChangePanel(baseParameterPanel));
            attackPanelButton.onClick.AddListener(() => ChangePanel(attackParameterPanel));
            movePanelButton.onClick.AddListener(() => ChangePanel(moveParameterPanel));

            closeButton.onClick.AddListener(()=>HidePanel());
        }

        public void ViewPanel()
        {
            enhancementPanel.SetActive(true);
            viewPanelSubject.OnNext(true);
        }

        public void HidePanel()
        {
            enhancementPanel.SetActive(false);
            viewPanelSubject.OnNext(false);
        }

        //所持しているポイントの表示
        public void SetPointText(int point)
        {
            foreach(var text in pointTextArray)
            {
                text.text= $"point:{point}";
            }
        }

        private void ChangePanel(GameObject panel)
        {
            currentOpenedPanel?.SetActive(false);
            panel.SetActive(true);
            currentOpenedPanel = panel;
        }

        //強化のボタンのが使用可能か設定する
        public void SetInteractableButtons(int currentPoint)
        {
            foreach (var UIs in enhansementUIElementsList)
            {
                foreach (var button in UIs.EnhancementButtonArray)
                {
                    button.Button.interactable = button.UsePoint <= currentPoint;
                }
            }
        }

        //パラメータの数値を表示
        public void SetParameterValue(PlayerParameterType playerParameterType, int value, float magnification)
        {
            var UI = enhansementUIElementsList.Single(x => x.PlayerParameterType == playerParameterType);
            if (UI == null)
            {
                Debug.LogError("パラメータに適したUIが設定されていません");
            }
            UI.SetValueText($"{value}(x{magnification.ToString(".00")})");
        }
    }
}


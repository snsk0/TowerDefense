using InGame.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.Enhancements
{
    public class EnhancementView : MonoBehaviour
    {
        [SerializeField] private GameObject enhancementPanel;
        [SerializeField] private Button maxHPOneUpButton;
        [SerializeField] private Button maxHPTenUpButton;
        [SerializeField] private Button attackValueOneUpButton;
        [SerializeField] private Button attackValueTenUpButton;
        [SerializeField] private Button defenceValueOneUpButton;
        [SerializeField] private Button defenceValueTenUpButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private TMP_Text pointText;

        private ISubject<KeyValuePair<PlayerParameterType, int>> parameterUpButtonClickSubject = new Subject<KeyValuePair<PlayerParameterType, int>>();
        public IObservable<KeyValuePair<PlayerParameterType, int>> parameterUpButtonClickObservable => parameterUpButtonClickSubject;

        private void Start()
        {
            maxHPOneUpButton.onClick.AddListener(() => parameterUpButtonClickSubject.OnNext(new KeyValuePair<PlayerParameterType, int>(PlayerParameterType.HP, 1)));
            maxHPTenUpButton.onClick.AddListener(() => parameterUpButtonClickSubject.OnNext(new KeyValuePair<PlayerParameterType, int>(PlayerParameterType.HP, 10)));
            attackValueOneUpButton.onClick.AddListener(() => parameterUpButtonClickSubject.OnNext(new KeyValuePair<PlayerParameterType, int>(PlayerParameterType.AttackPower, 1)));
            attackValueTenUpButton.onClick.AddListener(() => parameterUpButtonClickSubject.OnNext(new KeyValuePair<PlayerParameterType, int>(PlayerParameterType.AttackPower, 10)));
            defenceValueOneUpButton.onClick.AddListener(() => parameterUpButtonClickSubject.OnNext(new KeyValuePair<PlayerParameterType, int>(PlayerParameterType.DefencePower, 1)));
            defenceValueTenUpButton.onClick.AddListener(() => parameterUpButtonClickSubject.OnNext(new KeyValuePair<PlayerParameterType, int>(PlayerParameterType.DefencePower, 10)));

            closeButton.onClick.AddListener(()=>HidePanel());
        }

        public void ViewPanel()
        {
            enhancementPanel.SetActive(true);
        }

        public void HidePanel()
        {
            enhancementPanel.SetActive(false);
        }

        public void SetPointText(int point)
        {
            pointText.text = $"point:{point}";
        }

        public void SetIntaractableOneUpButton(bool value)
        {
            maxHPOneUpButton.interactable = value;
            attackValueOneUpButton.interactable = value;
            defenceValueOneUpButton.interactable = value;
        }

        public void SetIntaractableTenUpButton(bool value)
        {
            maxHPTenUpButton.interactable = value;
            attackValueTenUpButton.interactable = value;
            defenceValueTenUpButton.interactable = value;
        }
    }
}


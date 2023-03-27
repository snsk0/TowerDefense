using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Players
{
    public class PlayerHPView : MonoBehaviour
    {
        [SerializeField] private Slider HPBar;
        [SerializeField] private TMP_Text HPValueText;

        public void ViewHPBar(float rate)
        {
            HPBar.value = rate;
        }

        public void ViewHPValue(int currentValue, int maxValue)
        {
            HPValueText.text = $"{currentValue}/{maxValue}";
        }
    }
}


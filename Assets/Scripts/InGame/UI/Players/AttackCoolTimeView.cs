using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.UI.Players
{
    public class AttackCoolTimeView : MonoBehaviour
    {
        [SerializeField] private Image specialAttackImage;
        [SerializeField] private TMP_Text specialAttackCoolTimeText;

        public void ViewSpecialAttackCoolTime(int time, float fill)
        {
            specialAttackCoolTimeText.text = time<=0 ? "" : time.ToString();
            specialAttackImage.fillAmount = fill;
        }
    }
}


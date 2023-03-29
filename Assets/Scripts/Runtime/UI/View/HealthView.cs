using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;



namespace Runtime.UI.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider mainSlider;
        [SerializeField] private Slider animationSlider;


        public void SetValue(float value)
        {
            //メインスライダーは即座に変更
            mainSlider.value = value;

            //サブスライダーをアニメーション
            DOTween.To(() => animationSlider.value,
                n => animationSlider.value = n,
                value,
                duration: 1.0f);
        }
    }
}

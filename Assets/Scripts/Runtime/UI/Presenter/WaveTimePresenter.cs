using UnityEngine;
using UnityEngine.UI;

using Runtime.Wave;
using Runtime.Wave.State;

using StateMachines;

using Runtime.Util;

namespace Runtime.UI.Presenter
{
    public class WaveTimePresenter : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Text text;
        [SerializeField] private Text gameOverText;
        [SerializeField] private Text clearText;
        [SerializeField] private WaveManager manager;


        private void Update()
        {
            //waveêîçXêV
            text.text = manager.wave.ToString();


            //sliderèàóù
            StateBase<WaveManager> state = manager.stateMachine.GetCurrentLeafState();
            if (state == null) return;
            if (state.GetType() == typeof(WaitState))
            {
                WaitState wait = (WaitState)state;
                slider.value = wait.currentTime / wait.waitTime;
            }
            else if (state.GetType() == typeof(MainState))
            {
                MainState main = (MainState)state;
                slider.value = 1.0f - (main.currentTime / main.waveTime);
            }
            else if (state.GetType() == typeof(GameOverState))
            {
                gameOverText.gameObject.SetActive(true);
            }
            else if(state.GetType() == typeof(ClearState))
            {
                clearText.gameObject.SetActive(true);
            }
            else
            {
                slider.value = 0;
            }
        }


    }
}

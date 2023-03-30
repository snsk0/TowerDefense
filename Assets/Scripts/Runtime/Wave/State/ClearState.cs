using UnityEngine;

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Wave.State
{
    public class ClearState : StateBase<WaveManager>
    {
        private ChangeScene change;
        public ClearState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard)
        {
            change = owner.GetComponent<ChangeScene>();
        }


        private float timer = 0;
        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer > 5.0f) change.OnButton();
        }
    }
}

using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Wave.State
{
    public class ClearState : StateBase<WaveManager>
    {
        public ClearState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard){}
    }
}

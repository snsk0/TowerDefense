using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Wave.State
{
    public class GameOverState : StateBase<WaveManager>
    {
        public GameOverState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard) { }
    }
}

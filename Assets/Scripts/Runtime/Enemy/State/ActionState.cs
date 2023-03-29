using StateMachines;
using StateMachines.BlackBoards;


namespace Runtime.Enemy.State
{
    public class ActionState : ParentStateBase<EnemyController>
    {
        public ActionState(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard) { }
    }
}

using StateMachines.BlackBoards;

namespace StateMachines
{
    //Parentであることを明記する必要ある？
    public abstract class ParentStateBase<T> : StateBase<T>
    {
        //内部ステートマシン
        public StateMachine<T> innerStateMachine { get; }

        //StateMachineを続きから再開するか
        public bool isResume = false;

        //コンストラクタ
        public ParentStateBase(T owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            innerStateMachine = new StateMachine<T>(owner, blackBoard);
        }


        //Update
        public override sealed void Update()
        {
            SelfUpdate();
            innerStateMachine.Tick();
        }

        //Start
        public override sealed void Start()
        {
            if (!isResume) innerStateMachine.Reset();
            SelfStart();
        }

        //End
        public override sealed void End()
        {
            if(innerStateMachine.currentState != null) innerStateMachine.currentState.End();    //GuardChangeStateと同じ理由
            SelfEnd();
        }

        //ガード
        public override sealed bool GuardChangeState(StateBase<T> nextState)
        {
            //自身をチェック
            bool notGuard = SelfGuardChangeState(nextState);

            //自身がtrueのとき子もチェック(子が先に切り替わっていた時、まだstartはtickから呼ばれていないのでnullになっている)
            if (innerStateMachine.currentState != null && notGuard) notGuard = innerStateMachine.currentState.GuardChangeState(nextState);

            //返す
            return notGuard;
        }


        //継承用
        protected virtual void SelfUpdate() { }
        protected virtual void SelfStart() { }
        protected virtual void SelfEnd() { }
        protected virtual bool SelfGuardChangeState(StateBase<T> nextState) { return true; }
    }
}

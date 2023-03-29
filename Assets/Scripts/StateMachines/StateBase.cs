using StateMachines.BlackBoards;

namespace StateMachines
{
    public abstract class StateBase<T>
    {
        //フィールド
        protected readonly T owner;                     //オーナー
        protected readonly IBlackBoard blackBoard;      //ブラックボード


        //コンストラクタ
        public StateBase(T owner, IBlackBoard blackBoard)
        {
            this.owner = owner;
            this.blackBoard = blackBoard;
        }



        //継承用
        public virtual void Update() { }
        public virtual void Start() { }
        public virtual void End() { }
        public virtual bool GuardChangeState(StateBase<T> nextState) { return true; }
    }
}

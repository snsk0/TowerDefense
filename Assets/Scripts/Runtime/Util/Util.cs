using StateMachines;


namespace Runtime.Util
{
    public static class Util
    {
        public static StateBase<T> GetCurrentLeafState<T>(this StateMachine<T> stateMachine)
        {
            StateBase<T> state = stateMachine.currentState;

            if (state == null) return state;

            //ステートが親である場合
            if(state is ParentStateBase<T>)
            {
                //親にキャストして、インナーステートマシンから現在ステートを再帰で取得
                ParentStateBase<T> parentState = (ParentStateBase<T>)state;
                state = parentState.innerStateMachine.GetCurrentLeafState<T>();
            }

            return state;
        }

    }
}


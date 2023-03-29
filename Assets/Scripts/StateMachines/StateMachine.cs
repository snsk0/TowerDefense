using System;
using System.Collections.Generic;

using StateMachines.BlackBoards;


namespace StateMachines
{
    public class StateMachine<T>
    {
        //遷移用クラス
        private class Transition
        {
            //フィールド
            public StateBase<T> nextState { get; }
            public Func<IReadOnlyBlackBoard, bool> condition { get; }       //遷移条件


            public Transition(StateBase<T> nextState, Func<IReadOnlyBlackBoard, bool> condition)
            {
                this.nextState = nextState;
                this.condition = condition;
            }
        }

        //フィールド
        public readonly T owner;                                                                 //オーナー
        private readonly IReadOnlyBlackBoard blackBoard;                                         //ブラックボード
        private readonly Dictionary<StateBase<T>, List<Transition>> transitionList;              //遷移リスト
        public StateBase<T> currentState { get; private set; }                                   //現在ステート
        private StateBase<T> initialState;                                                       //初期ステート





        //コンストラクタ
        public StateMachine(T owner, IReadOnlyBlackBoard blackBoard)
        {
            this.owner = owner;
            this.blackBoard = blackBoard;

            transitionList = new Dictionary<StateBase<T>, List<Transition>>();
        }





        //初期セットアップ
        public void Initialize(StateBase<T> state)
        {
            initialState = state;
            //stateにawakeを実装するならここで一括
        }
        //リセット
        public void Reset()
        {
            currentState = null;
            //stateにawakeを実装するならここで一括
        }




        //更新メソッド
        public void Tick()
        {
            //現在ステートがnullの場合
            if(currentState == null)
            {
                //初期化してstart呼び出し
                currentState = initialState;
                currentState.Start();
            }


            //Updateの呼び出し
            currentState.Update();


            //遷移チェック
            StateBase<T> nextState = CanTransition(currentState);
            if (nextState != null) ChangeState(nextState);
        }





        //遷移追加
        public void AddTransition(StateBase<T> state, StateBase<T> nextState, Func<IReadOnlyBlackBoard, bool> condition)
        {
            //遷移オブジェクトの生成
            Transition transition = new Transition(nextState, condition);

            //遷移元があるかどうか
            if (transitionList.ContainsKey(state))
            {
                //リストを取得して追加
                transitionList[state].Add(transition);
            }

            else
            {
                //リストを生成して追加する
                List<Transition> valueList = new List<Transition>();
                valueList.Add(transition);
                transitionList.Add(state, valueList);
            }
        }






        //ステート変更
        private void ChangeState(StateBase<T> nextState)
        {
            //現在ステートの終了を通知
            currentState.End();

            //次ステートに変更する
            currentState = nextState;

            //スタート呼び出し
            currentState.Start();
        }


        //遷移の確認 できないならnull
        private StateBase<T> CanTransition(StateBase<T> state)
        {
            if (transitionList.ContainsKey(state))
            {
                //全ての条件を上からチェック
                foreach (Transition transition in transitionList[state])
                {
                    //次の遷移先に対するガードがないか
                    bool guard = state.GuardChangeState(transition.nextState);

                    //遷移が可能か確認
                    bool flag = transition.condition.Invoke(blackBoard);
                    if (flag && guard) return transition.nextState;
                }
            }

            return null;
        }
    }
}

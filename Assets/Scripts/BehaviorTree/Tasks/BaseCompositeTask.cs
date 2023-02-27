using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Tasks
{
    //再評価形式
    public enum AbortType
    {
        None,           //無し
        Self,           //自身の子のみ
        LowPriority,    //自身の後に実行されている時のみ
        Both            //SelfとLowPriority
    }



    public abstract class BaseCompositeTask : BaseTask
    {
        //子タスク
        [SerializeField] private List<BaseTask> _children = new List<BaseTask>();
        public List<BaseTask> children => _children;
        public int currentChildIndex{ get; protected set; }

        //再評価タイプ
        [SerializeField] private AbortType abortType;




        //サブクラス用
        public virtual bool CanExcute() { return true; }                            //実行可能か
        public virtual void OnChildStarted() { }                                    //子が開始した時に呼び出される
        public virtual void OnChildExecuted(TaskStatus childStatus) { }             //子が実行され終わったときに呼び出す
        public virtual void OnConditionalAbort(int childIndex) { }                  //再評価で中断された時
    }
}

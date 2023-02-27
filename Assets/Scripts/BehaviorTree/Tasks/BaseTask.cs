using UnityEngine;

namespace BehaviorTree.Tasks
{
    //タスクの基底クラス
    public abstract class BaseTask : MonoBehaviour
    {
        //フィールド
        public GameObject owner { get; private set; }       //BehaviorTreeのアタッチ先
        public int index { get; private set; }              //TaskのIndex番号
        //public BaseTask parent { get; private set; }      //このタスクの親タスク nullならroot
        [SerializeField] private string taskName;           //デバッグ用 タスクの名前



        

        //サブクラス用
        public virtual void OnAwake() { }       //Tree起動時
        public virtual void OnStart() { }       //Task起動時
        public virtual void OnEnd() { }         //Task終了時(Success or Failure を返すとき)
        public virtual TaskStatus OnUpdate() { return TaskStatus.Inactive; }    //毎フレーム





        //初期化
        public void Initialize(GameObject owner, int index)
        {
            this.owner = owner;
            this.index = index;
        }
    }
}

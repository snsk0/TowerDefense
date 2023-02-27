using System.Collections.Generic;
using UnityEngine;
using BehaviorTree.Tasks;

namespace BehaviorTree
{
    public class SimpleBehaviorTree : MonoBehaviour
    {
        //基底タスク
        [SerializeField] private BaseTask rootTask;

        //ActiveTaskのIndex
        private Stack<int> activeStack = new Stack<int>();      //現在アクティブのタスクのスタック
        private int activeTaskIndex                             //現在アクティブのタスク
        {
            get
            {
                if(activeStack.Count > 0) return activeStack.Peek();
                return -1;
            }
        }

        //List
        private List<BaseTask> taskList = new List<BaseTask>();

        //BTStatus
        private bool isRootTaskExcuted = false;
        [SerializeField] private bool isLoop;



        //Stack管理
        //Push
        private void PushTask(BaseTask task)
        {
            if(activeStack.Count == 0 || activeTaskIndex != task.index)
            {
                //スタックに積む
                activeStack.Push(task.index);

                //スタート呼び出し
                task.OnStart();
            }
        }

        //Pop
        private void PopTask()
        {
            if (activeStack.Count > 0)
            {
                //スタックから取り出す
                int index = activeStack.Pop();

                //終了呼び出し
                taskList[index].OnEnd();
            }
        }
        




        //基底クラスからInitializeとAwakeを呼ぶ インデックス番号の振り分けも行う
        private void CallOnAwake(BaseTask task)
        {
            //タスクの初期化
            task.Initialize(gameObject, taskList.Count);

            //リスト
            taskList.Add(task);

            //Awake
            task.OnAwake();

            //Compositeは再帰処理
            BaseCompositeTask compositeTask = task as BaseCompositeTask;
            if(compositeTask != null)
            {
                foreach(BaseTask child in compositeTask.children)
                {
                    CallOnAwake(child);
                }
            }
        }



        //タスクの実行
        private TaskStatus Excute(BaseTask task)
        {
            //ActiveStackにプッシュ
            PushTask(task);

            //ステータス
            TaskStatus status = TaskStatus.Inactive;    //デフォルトでInactiveを返す


            //Composite
            if (task is BaseCompositeTask)
            {
                BaseCompositeTask compositeTask = task as BaseCompositeTask;


                //実行可能だったら実行し続ける
                while (compositeTask.CanExcute())
                {
                    //TODO 再評価リストに登録

                    //親のOnStarted呼び出し
                    compositeTask.OnChildStarted();

                    //子を再帰で実行する
                    BaseTask child = compositeTask.children[compositeTask.currentChildIndex];
                    TaskStatus childStatus = Excute(child);

                    //Runnningの場合中断
                    if (childStatus == TaskStatus.Running) return TaskStatus.Running;

                    status = childStatus;
                }
            }

            //Task
            else
            {
                //Update
                status = task.OnUpdate();

                //Runnningの場合は中断
                if (status == TaskStatus.Running) return TaskStatus.Running;
            }


            //終了処理
            PopTask();

            //Pop後にCompositeTaskだった場合は子の終了通知
            if (activeStack.Count > 0)
            {
                BaseCompositeTask compositeActiveTask = taskList[activeTaskIndex] as BaseCompositeTask;
                if (compositeActiveTask != null) compositeActiveTask.OnChildExecuted(status);
            }

            return status;
        }





        //UnityEngineで動かす
        private void Start()
        {
            CallOnAwake(rootTask);
        }

        //TODO タスク実行のフレーム管理
        //現状、子タスクでRunnningが出た場合、それが次フレームでSuccessしてもそのまま帰ってきて次の子は更に次フレームという仕様になっている
        //実行されるフレームや順番は極力細かく制御する必要がある
        private void Update()
        {
            if (activeTaskIndex > -1)
            {
                BaseTask task = taskList[activeTaskIndex];
                Excute(task);
            }
            else if(!isRootTaskExcuted || isLoop)
            {
                Excute(rootTask);
                isRootTaskExcuted = true;
            }
        }




        //子タスクと親タスクについて...
        //まず、親タスクが子タスクを持てることについて、子タスクについては必ずIndexでなく実体を持つ必要がある
        //これは親タスクが子タスクに何らかの影響を与えることができるようにするためである
        //例えばDecoratorなどで100回実行する、などがこれに該当する。子タスクを実行できなきゃいけない。
        //親タスクについて、子から親を検索できないと再評価ができない
        //これについて、子タスクは親タスクを知らずにBT側で管理するように....でもよいが
        //子から親をなんらかの手段で検索できるようにしておかないと困るのでこれの実装も行う


        //それか...
        //最低レベルAPIとして1つのBehaviorTreeに必ずIndex番号とTaskの照会を行えるクラスを作ってBTに持たせる
        //タスクも初期化時にアクセスできるようにして要素として必ずインデックスを持つよう共通化しておく
        //ただタスクの役割が何なのかよくわからなくなったりする....

    }
}

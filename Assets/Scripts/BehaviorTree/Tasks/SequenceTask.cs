namespace BehaviorTree.Tasks
{
    public class SequenceTask : BaseCompositeTask
    {
        //現在の状態
        TaskStatus status = TaskStatus.Inactive;


        public override bool CanExcute() 
        {
            return children.Count > currentChildIndex && status != TaskStatus.Failure; 
        }





        public override void OnChildExecuted(TaskStatus childStatus) 
        {
            currentChildIndex++;
            status = childStatus;
        }




        public override void OnEnd()
        {
            currentChildIndex = 0;
            status = TaskStatus.Inactive;
        }




        //再評価の中断時に呼び出される
        //自身の子が中断された時に呼び出される
        //childIndexを自身の該当する子まで戻す
        //また、中断された時、中断したConditionalタスクから再帰的に共通祖先までchildIndexを戻す必要がある
        public override void OnConditionalAbort(int childIndex)
        {
            //TODO
        }
    }
}

using UnityEngine;
using UnityEngine.AI;



namespace Runtime.Enemy.Component
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        //設定項目
        [SerializeField] private float rangeByTarget;          //ターゲットにどこまで近づくか
        [SerializeField] private bool canRaycast;              //障害物があるときに移動を終了するか        

        //移動済みフラグ
        private bool moved;



        //移動関数
        public bool MoveToTarget(Vector3 targetPosition)
        {
            //範囲チェック
            if (Vector3.Distance(agent.transform.position, targetPosition) < rangeByTarget)
            {
                //rayCastによるチェックを行わない場合終了
                if (!canRaycast) return false;

                //TODO Raycastによる障害物のチェック
            }


            //agentを設定
            moved = true;
            agent.isStopped = false;
            agent.SetDestination(targetPosition);
            return true;
        }




        //フラグ初期化
        private void OnEnable()
        {
            moved = false;
            agent.isStopped = false;
        }

        //フラグ更新
        private void Update()
        {
            if (moved) moved = false;
            else agent.isStopped = true;
        }
    }

}
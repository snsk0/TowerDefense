using UnityEngine;
using UnityEngine.AI;



namespace Runtime.Enemy.Component
{
    public class EnemySeek : MonoBehaviour
    {
        //コンポーネント
        [SerializeField] private EnemyMove move;
        [SerializeField] private EnemyLook look;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private NavMeshObstacle obstacle;
        [SerializeField] private new Rigidbody rigidbody;

        //パラメータ
        [SerializeField] private float fixedAgentDistance;
        [SerializeField] private float stoppingAgentDistance;
        [SerializeField] private float stoppingVelocity;

        //フィールド
        private Transform target;

        //フラグ
        public bool isSeeking { get; private set; }
        public bool isFailed { get; private set; }

        
        //外部から変更可能
        public float stoppingDistance { set { agent.stoppingDistance = value - fixedAgentDistance; } }



        //初期化
        private void Start()
        {
            agent.isStopped = true;
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.enabled = false;

            obstacle.enabled = true;

            isSeeking = false;
            isFailed = false;
        }



        //追尾開始
        public void StartSeek(Transform target)
        {
            //ターゲットを更新
            this.target = target;

            //フラグを初期化
            isSeeking = true;
            isFailed = false;

            //各コンポーネントを有効化
            move.enabled = true;
            obstacle.enabled = false;
            agent.enabled = true;
            agent.isStopped = false;
        }
        //終了
        public void EndSeek()
        {
            //実行されていた場合のみ処理
            if(isSeeking == true)
            {
                //フラグを無効化
                isSeeking = false;

                //各コンポーネントを無効化
                move.enabled = false;
                agent.isStopped = true;
                agent.enabled = false;
                obstacle.enabled = true;
            }
        }



        private void Update()
        {
            if (!isSeeking) return;

            //目的地登録
            agent.SetDestination(target.position);

            //たどり着けるか判定(pendingより先に判定)
            if (agent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                isFailed = true;
                EndSeek();
                return;
            }

            //パスの計算が終了しているか
            if (agent.pathPending == true) return;


            //距離がぶれるので別で調整
            /*
            //agentの終了を判定
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //agentとの距離を測定
                if (Vector3.Distance(agent.nextPosition, transform.position) < stoppingAgentDistance)
                {
                    //速度が一定以下になったら終了
                    if (rigidbody.velocity.magnitude <= stoppingVelocity)
                    {
                        EndSeek();
                        return;
                    }
                }

                //agentに近づく
                else
                {
                    //移動と回転入力
                    move.MoveByWorldDir(agent.nextPosition - transform.position);
                    look.Look(agent.nextPosition);
                }
            }
            */

            //agent座標を一定距離,行きたい方向に
            Vector3 direction = agent.nextPosition - transform.position;
            direction = direction.normalized * fixedAgentDistance;
            agent.nextPosition = transform.position + direction;


            //Y軸無視で距離を判定
            if (transform.position.DistanceIgnoreY(target.position) < agent.stoppingDistance + fixedAgentDistance)
            {
                //速度が一定以下になったら終了
                if (rigidbody.velocity.magnitude <= stoppingVelocity)
                {
                    EndSeek();
                    return;
                }
            }

            //距離がはなれていたら実行し続ける
            else
            {
                //移動と回転入力
                move.MoveByWorldDir(agent.nextPosition - transform.position);
                look.Look(agent.nextPosition);
            }
        }


    }
}


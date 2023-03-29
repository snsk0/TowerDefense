using UnityEngine;
using UnityEngine.AI;

using Runtime.Enemy.Component;

using StateMachines;
using StateMachines.BlackBoards;



namespace Runtime.Enemy.State
{
    public class SeekState : StateBase<EnemyController>
    {
        //設定変数
        public float minVelocity { private get; set; }
        public float maxAgentDistance { private get; set; }
        public float followAgentDistance { private get; set; }
        public bool isFixedAgentDistance { private get; set; } = false;

        //privateフィールド
        private NavMeshAgent agent;
        private Rigidbody rigidbody;
        private EnemyLook look;
        private ITargetProvider targetProvider;
        private GameObject target;
        private EnemyMove move;
        private LineRenderer renderer;
        private NavMeshObstacle obstacle;


        //コンストラクタ
        public SeekState(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            targetProvider = owner.GetComponent<ITargetProvider>();
            look = owner.GetComponent<EnemyLook>();
            agent = owner.GetComponent<NavMeshAgent>();
            rigidbody = owner.GetComponent<Rigidbody>();
            move = owner.GetComponent<EnemyMove>();
            renderer = owner.GetComponent<LineRenderer>();
            obstacle = owner.GetComponent<NavMeshObstacle>();


            //agentの同期を切る
            //agent.updatePosition = false;
            //agent.updateRotation = false;
            //agent.updateUpAxis = false;
            agent.isStopped = true;

        }




        //初期化
        public override void Start()
        {
            move.enabled = false;
            target = targetProvider.target.Value;
            agent.isStopped = false;
            obstacle.enabled = false;
        }

        //終了
        public override void End()
        {
            agent.isStopped = true;
            move.enabled = false;
            //obstacle.enable = true;
        }


        public override void Update()
        {
            //目的地の更新
            agent.SetDestination(target.transform.position);

            //パスの計算準備ができている場合
            if (agent.pathPending == true) return;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {

            }

            renderer.SetPositions(agent.path.corners);
            /*
            //距離判定
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //速度を判定
                if (agent.velocity.magnitude <= 0)
                {
                    blackBoard.SetValue<bool>("Seek", false);
                }
            }
            else
            {
                owner.transform.position = agent.nextPosition;
                look.Look(agent.steeringTarget);
            }
            */
            


            //agent.velocity = rigidbody.velocity;

            //agent座標を一定距離に補正する(一定に保つと後ろ向きに移動しない)
            //挙動調整 agentとの距離を一定にする(maxAgentDistanceに固定するかどうか
            /*
            Vector3 direction = agent.nextPosition - owner.transform.position;
            direction = direction.normalized * maxAgentDistance;
            agent.nextPosition = owner.transform.position + direction;


            //距離の判定(agent)と、追従距離の判定
            if (agent.remainingDistance <= agent.stoppingDistance - maxAgentDistance)
            {
                //速度を判定
                if (rigidbody.velocity.magnitude <= minVelocity)
                {
                    blackBoard.SetValue<bool>("Seek", false);
                }
            }
            else
            {
                //移動と回転入力
                move.MoveByWorldDir(agent.nextPosition - owner.transform.position);
                look.Look(agent.steeringTarget);
            }
            */
        }


    }
}

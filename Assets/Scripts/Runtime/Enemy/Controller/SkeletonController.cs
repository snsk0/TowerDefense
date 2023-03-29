using UnityEngine;

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy.Animation;
using Runtime.Enemy.Component;


namespace Runtime.Enemy.Controller
{
    /*
    public class SkeletonController : EnemyController
    {
        [SerializeField] private GameObject player;
        [SerializeField] private EnemyHate hate;

        //初期化
        private void OnEnable()
        {
            blackBoard = new BlackBoard();
            stateMachine = new StateMachine<EnemyController>(this, blackBoard);


            hate.AddHate(100f, player);




            //ステートマシン構築

            //battleMoveセットアップ
            BattleMove battleMove = new BattleMove(this, blackBoard);
            Idle idle = new Idle(this, blackBoard);
            RandomWalk randomWalk = new RandomWalk(this, blackBoard);
            FrontWalk frontWalk = new FrontWalk(this, blackBoard);
            BackWalk backWalk = new BackWalk(this, blackBoard);
            Taunt taunt = new Taunt(this, blackBoard);


            battleMove.range = 4.5f;
            battleMove.attackCoolTime = 3.5f;
            idle.minWaitTime = 0.3f;
            idle.maxWaitTime = 1.5f;
            idle.farRange = 3.8f;
            idle.nearRange = 2.2f;
            randomWalk.minWalkTime = 2f;
            randomWalk.maxWalkTime = 3f;
            randomWalk.verticalWalkTime = 0.4f;
            randomWalk.tauntRandom = 0f;
            randomWalk.avoidRange = 2.0f;
            frontWalk.range = 3.0f;
            backWalk.range = 3.0f;


            battleMove.innerStateMachine.AddTransition(idle, randomWalk, blackBoard => { return (blackBoard.GetValue<bool>("RandomWalk") == true); });
            battleMove.innerStateMachine.AddTransition(randomWalk, taunt, blackBoard => { return (blackBoard.GetValue<bool>("Taunt") == true); });  //tauntへの遷移をidleより優先
            battleMove.innerStateMachine.AddTransition(randomWalk, backWalk, blackBoard => { return (blackBoard.GetValue<bool>("BackWalk") == true); }); //backをidleより優先
            battleMove.innerStateMachine.AddTransition(randomWalk, idle, blackBoard => { return (blackBoard.GetValue<bool>("RandomWalk") == false); });
            battleMove.innerStateMachine.AddTransition(taunt, idle, blackBoard => { return (blackBoard.GetValue<bool>("Taunt") == false); });
            battleMove.innerStateMachine.AddTransition(idle, frontWalk, blackBoard => { return (blackBoard.GetValue<bool>("FrontWalk") == true); });
            battleMove.innerStateMachine.AddTransition(frontWalk, idle, blackBoard => { return (blackBoard.GetValue<bool>("FrontWalk") == false); });
            battleMove.innerStateMachine.AddTransition(idle, backWalk, blackBoard => { return (blackBoard.GetValue<bool>("BackWalk") == true); });
            battleMove.innerStateMachine.AddTransition(backWalk, idle, blackBoard => { return (blackBoard.GetValue<bool>("BackWalk") == false); });


            battleMove.innerStateMachine.Initialize(idle);

            blackBoard.SetValue<bool>("RandomWalk", false);
            blackBoard.SetValue<bool>("FrontWalk", false);
            blackBoard.SetValue<bool>("BackWalk", false);
            blackBoard.SetValue<bool>("Taunt", false);
            blackBoard.SetValue<bool>("Attack", true);


            //ステートをインスタンス化
            Seek seek = new Seek(this, blackBoard);
            Attack attack = new Attack(this, blackBoard);


            //遷移を設定
            stateMachine.AddTransition(battleMove, seek, blackBoard => { return (blackBoard.GetValue<bool>("Seek") == true); });
            stateMachine.AddTransition(seek, battleMove, blackBoard => { return (blackBoard.GetValue<bool>("Seek") == false); });
            stateMachine.AddTransition(battleMove, attack, blackBoard => { return (blackBoard.GetValue<bool>("Attack") == true); });
            stateMachine.AddTransition(attack, battleMove, blackBoard => { return (blackBoard.GetValue<bool>("Attack") == false); });
            //stateMachine.AddTransition(seek, atk, b => { return !b.GetValue<bool>("move"); });
            //stateMachine.AddTransition(atk, seek, b => { return b.GetValue<bool>("move"); });


            //ブラックボードの初期値を設定(値がない場合Default値が返されて意図しない挙動をすることがあるため
            blackBoard.SetValue<bool>("Seek", false);

            stateMachine.Initialize(battleMove);
        }

        private void Update()
        {
            stateMachine.Tick();
        }
    }





    //追跡ステート
    public class Seek : StateBase<EnemyController>
    {
        //必要コンポーネント
        private EnemySeek seek;
        private EnemyHate hate;


        //初期化
        public Seek(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            seek = owner.GetComponent<EnemySeek>();
            hate = owner.GetComponent<EnemyHate>();
        }



        public override void Start()
        {
            //seekの呼び出し
            seek.StartSeek(hate.GetMaxHateObject().transform);
        }


        public override void Update()
        {
            //追跡が終了したらfalseにする
            if (!seek.isSeeking) blackBoard.SetValue<bool>("Seek", false);
        }
    }





    //戦闘中の移動
    public class BattleMove : ParentStateBase<EnemyController>
    {
        //必要コンポーネント
        private EnemyHate hate;
        private GameObject target;


        //パラメータ
        public float range { private get; set; }
        public float attackCoolTime { private get; set; }
        private float timer;


        //初期化
        public BattleMove(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            hate = owner.GetComponent<EnemyHate>();
        }


        protected override void SelfStart()
        {
            target = hate.GetMaxHateObject();
            timer = 0;
        }

        protected override void SelfUpdate()
        {
            //追跡距離を判定(Y軸無視)
            if (owner.transform.position.GetDistanceIgnoreY(target.transform.position) > range)
            {
                blackBoard.SetValue<bool>("Seek", true);
                return;
            }

            //攻撃を判定
            timer += Time.deltaTime;
            if (timer > attackCoolTime) blackBoard.SetValue<bool>("Attack", true);
        }
    }





    //立ちステート
    public class Idle : StateBase<EnemyController>
    {
        //要求コンポーネント
        private EnemyHate hate;
        private EnemyLook look;
        private EnemyAnimator animator;
        private GameObject target;


        //パラメータ
        public float minWaitTime { private get; set; }
        public float maxWaitTime { private get; set; }
        public float farRange { private get; set; }
        public float nearRange { private get; set; }


        //待機時間
        private float waitTime;


        //初期化
        public Idle(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            hate = owner.GetComponent<EnemyHate>();
            look = owner.GetComponent<EnemyLook>();
            animator = owner.GetComponent<EnemyAnimator>();
        }



        public override void Start()
        {
            //targetを取得
            target = hate.GetMaxHateObject();

            //待機時間を生成
            waitTime = Random.Range(minWaitTime, maxWaitTime);

            animator.PlayIdle();
        }


        public override void Update()
        {
            //対象を見る
            look.Look(target.transform);

            //距離測定
            float distance = owner.transform.position.GetDistanceIgnoreY(target.transform.position);

            
            //遠い場合
            if(distance > farRange)
            {
                blackBoard.SetValue<bool>("FrontWalk", true);
                return;
            }
            //近い
            else if(distance < nearRange)
            {
                blackBoard.SetValue<bool>("BackWalk", true);
                return;
            }


            //待機時間減少
            waitTime -= Time.deltaTime;

            //待機時間が終了した場合
            if(waitTime < 0)
            {
                blackBoard.SetValue<bool>("RandomWalk", true);
            }
        }
    }





    //ランダム移動
    public class RandomWalk : StateBase<EnemyController>
    {
        //コンポーネント
        private Enemy.Component.EnemyMove move;
        private EnemyLook look;
        private EnemyHate hate;
        private GameObject target;


        //パラメータ
        public float minWalkTime { private get; set; }
        public float maxWalkTime { private get; set; }
        public float verticalWalkTime { private get; set; }
        public float tauntRandom { private get; set; }
        public float avoidRange { private get; set; }
        private float walkTime;
        private Vector3 direction;
        private bool tauntFlag;


        //初期化
        public RandomWalk(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            move = owner.GetComponent<Enemy.Component.EnemyMove>();
            look = owner.GetComponent<EnemyLook>();
            hate = owner.GetComponent<EnemyHate>();
        }



        //targetの取得
        public override void Start()
        {
            target = hate.GetMaxHateObject();

            //実行時間を決める
            walkTime = Random.Range(minWalkTime, maxWalkTime);

            //乱数で方向を決める
            float ran = Random.Range(0, 4);
            if (ran < 1.0f) direction = Vector3.forward;
            else if (ran < 2.0f) direction = -Vector3.forward;
            else if (ran < 3.0f) direction = Vector3.right;
            else direction = -Vector3.right;

            //前後の場合補正値をかける
            if (direction.z != 0) walkTime *= verticalWalkTime;

            //威嚇の成功判定フラグ
            tauntFlag = true;
        }


        public override void Update()
        {
            //対象を見る
            look.Look(target.transform);

            //距離が近すぎたらはなれる
            if (owner.transform.position.GetDistanceIgnoreY(target.transform.position) < avoidRange)
            {
                blackBoard.SetValue<bool>("BackWalk", true);
                blackBoard.SetValue<bool>("RandomWalk", false);
                return;
            }

            //移動
            move.Move(direction);

            //待機時間減少
            walkTime -= Time.deltaTime;

            //待機時間が終了した場合
            if (walkTime < 0)
            {
                if(Random.value < tauntRandom && tauntFlag) blackBoard.SetValue<bool>("Taunt", true);
                blackBoard.SetValue<bool>("RandomWalk", false);
                tauntFlag = false;
            }
        }



        //攻撃ステートへの変更をブロック
        public override bool GuardChangeState(StateBase<EnemyController> nextState)
        {
            if (false) return false;
            return true;
        }
    }





    //前方移動
    public class FrontWalk : StateBase<EnemyController>
    {
        //コンポーネント
        private Enemy.Component.EnemyMove move;
        private EnemyLook look;
        private EnemyHate hate;

        //パラメータ
        public float range { private get; set; }
        private GameObject target;


        //初期化
        public FrontWalk(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            move = owner.GetComponent<Enemy.Component.EnemyMove>();
            look = owner.GetComponent<EnemyLook>();
            hate = owner.GetComponent<EnemyHate>();
        }



        public override void Start()
        {
            target = hate.GetMaxHateObject();
        }

        public override void Update()
        {
            //対象を見る
            look.Look(target.transform);

            //距離判定
            if(owner.transform.position.GetDistanceIgnoreY(target.transform.position) < range)
            {
                //遷移
                blackBoard.SetValue<bool>("FrontWalk", false);
                return;
            }

            //移動
            move.Move(Vector3.forward);
        }
    }




    public class BackWalk : StateBase<EnemyController>
    {
        //コンポーネント
        private Enemy.Component.EnemyMove move;
        private EnemyLook look;
        private EnemyHate hate;

        //パラメータ
        public float range { private get; set; }
        private GameObject target;


        //初期化
        public BackWalk(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            move = owner.GetComponent<Enemy.Component.EnemyMove>();
            look = owner.GetComponent<EnemyLook>();
            hate = owner.GetComponent<EnemyHate>();
        }



        public override void Start()
        {
            target = hate.GetMaxHateObject();
        }

        public override void Update()
        {
            //対象を見る
            look.Look(target.transform);

            //距離判定
            if (owner.transform.position.GetDistanceIgnoreY(target.transform.position) > range)
            {
                //遷移
                blackBoard.SetValue<bool>("BackWalk", false);
                return;
            }

            //移動
            move.Move(-Vector3.forward);
        }
    }




    public class Taunt : StateBase<EnemyController>
    {
        //コンポーネント
        private EnemyAnimator animator;

        private float waitTime = 1.5f;
        private float deltTime;

        //初期化
        public Taunt(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            animator = owner.GetComponent<EnemyAnimator>();
        }



        public override void Start()
        {
            animator.PlayAttack(0);
            deltTime = 0;
        }

        public override void Update()
        {
            deltTime += Time.deltaTime;

            //距離判定
            if (waitTime < deltTime)
            {
                //遷移
                blackBoard.SetValue<bool>("Taunt", false);
                return;
            }
        }
    }





    public class Attack : StateBase<EnemyController>
    {
        //コンポーネント
        private Runtime.LegacyEnemy.Component.EnemyAttack attack;
        private EnemyHate hate;

        private float time;

        public Attack(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            attack = owner.GetComponent<LegacyEnemy.Component.EnemyAttack>();
            hate = owner.GetComponent<EnemyHate>();
        }



        public override void Start()
        {
            time = 3.0f;
            attack.AttackToTarget(hate.GetMaxHateObject(), 0);
        }

        public override void Update()
        {
            time -= Time.deltaTime;

            if (time < 0) blackBoard.SetValue<bool>("Attack", false);
        }
    }





    public class TargetPlayer : ParentStateBase<EnemyController>
    {
        private EnemyHate hate;

        public TargetPlayer(EnemyController controller, IBlackBoard blackBoard) : base(controller, blackBoard)
        {
            hate = owner.GetComponent<EnemyHate>();
        }



        protected override void SelfUpdate() 
        {
            GameObject target = hate.GetMaxHateObject();

            if (target.tag == "Tower") blackBoard.SetValue<bool>("TowerTarget", true);
        }
    }
    */



}
public static class ExtensionVector3
{
    public static float DistanceIgnoreY(this Vector3 origin, Vector3 target)
    {
        Vector3 distance = (target - origin);
        distance.y = 0;

        return distance.magnitude;
    }
}

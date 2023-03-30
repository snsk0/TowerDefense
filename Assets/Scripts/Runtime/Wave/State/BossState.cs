using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy;
using Runtime.Enemy.Component;

namespace Runtime.Wave.State
{
    public class BossState : StateBase<WaveManager>
    {
        //コンポーネント
        private EnemyManager manager;
        private EnemyHealth health;

        public EnemyType type { private get; set; }


        //コンストラクタ
        public BossState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard)
        {
            this.manager = owner.GetComponent<EnemyManager>();
        }



        public override void Start()
        {
            //ボスをスポーンさせる
            EnemyController controller = manager.GetInitialEnemy(type);
            health = controller.GetComponent<EnemyHealth>();
        }

        public override void Update()
        {
            //ボスが生きてるかどうか、死んだら以降
            if(health.currentHealth.Value == 0)
            {
                blackBoard.SetValue<bool>("Boss", false);
            }
        }

    }
}

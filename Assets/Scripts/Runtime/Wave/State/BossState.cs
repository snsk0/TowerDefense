using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy;
using Runtime.Enemy.Component;

namespace Runtime.Wave.State
{
    public class BossState : StateBase<WaveManager>
    {
        //コンポーネント
        private IEnemyGenerator generator;
        private EnemyHealth health;


        //コンストラクタ
        public BossState(WaveManager manager, IBlackBoard blackBoard) : base(manager, blackBoard)
        {
            foreach(IEnemyGenerator generator in owner.GetComponents<IEnemyGenerator>())
            {
                if (generator.enemyType == EnemyType.Golem) this.generator = generator;
            }
        }



        public override void Start()
        {
            //ボスをスポーンさせる
            health =  generator.Generate(owner.transform).GetComponent<EnemyHealth>();
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

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy.Component;


namespace Runtime.Enemy.State
{
    public class SeekStateByComponent : StateBase<EnemyController>
    {
        //コンポーネント
        private EnemySeek seek;
        private ITargetProvider targetProvider; //追跡不可能な場合ヘイトを書き換えるため

        //パラメータ
        public float stoppingDistance { private get; set; }


        //コンストラクタ
        public SeekStateByComponent(EnemyController owner, IBlackBoard blackBoard) : base(owner, blackBoard)
        {
            seek = owner.GetComponent<EnemySeek>();
            targetProvider = owner.GetComponent<ITargetProvider>();
        }



        public override void Start()
        {
            //stoppingDistanceを先に設定する
            seek.stoppingDistance = stoppingDistance;
            seek.StartSeek(targetProvider.target.Value.transform);
        }

        public override void Update()
        {
            //経路探索に失敗した場合
            if (seek.isFailed) blackBoard.SetValue<bool>("SeekFailed", true);
            else if (!seek.isSeeking) blackBoard.SetValue<bool>("Seek", false);
        }

        public override void End()
        {
            //中断された時にSeekも中断する
            seek.EndSeek();
        }
    }
}


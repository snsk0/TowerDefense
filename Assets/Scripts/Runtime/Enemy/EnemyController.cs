using UnityEngine;

using UniRx;

using StateMachines;
using StateMachines.BlackBoards;

using Runtime.Enemy.Parameter;
using Runtime.Enemy.Component;



namespace Runtime.Enemy
{
    public abstract class EnemyController : MonoBehaviour, IDamagable
    {
        //ステートマシン関連
        protected StateMachine<EnemyController> stateMachine;
        protected IBlackBoard blackBoard = new BlackBoard();


        //コンポーネント
        [SerializeField] protected EnemyHealth health;
        [SerializeField] protected EnemyHate hate;
        [SerializeField] private EnemyParameter _parameter;
        public EnemyParameter parameter => _parameter;


        //死亡フラグ
        private ReactiveProperty<bool> _isDeathProperty;
        public IReadOnlyReactiveProperty<bool> isDeathProperty => _isDeathProperty;


        //初期化
        protected void OnEnable()
        {
            stateMachine.Reset();
            parameter.Initialize(0);
            health.Initialize();
            hate.Initialize();
            _isDeathProperty = new ReactiveProperty<bool>(false);
        }

        //Property破棄
        protected void OnDisable()
        {
            //死亡フラグpropertyをDispose
            _isDeathProperty.Dispose();

            //外からアクセスしたblackBoardのvalueを初期化しておく
            blackBoard.SetValue<Vector3>("Damaged", Vector3.zero);
            blackBoard.SetValue<bool>("Death", false);
        }


        //ステートマシン更新
        protected void Update()
        {
            stateMachine.Tick();
        }


        //ダメージ関数
        public void Damage(float damage, Vector3 knock, float hate, GameObject cause)
        {
            health.SetDamage(damage);
            this.hate.AddHate(hate, cause);

            //死亡判定
            _isDeathProperty.Value = isDeath();
            if (isDeathProperty.Value)
            {
                blackBoard.SetValue<bool>("Death", true);
            }
            else
            {
                blackBoard.SetValue<Vector3>("Damaged", knock);
            }
        }



        //死亡判定
        protected virtual bool isDeath()
        {
            if(health.currentHealth.Value <= 0)
            {
                return true;
            }
            return false;
        }


    }
}

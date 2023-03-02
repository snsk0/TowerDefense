using System;

using UnityEngine;

using UniRx;

using Runtime.Enemy.Component;
using Runtime.Enemy.Parameter;


namespace Runtime.Enemy 
{
    public class EnemyController : MonoBehaviour, IEnemyDamagable, IEnemyEventSender
    {
        //敵のコンポーネント群
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyHate hate;
        [SerializeField] private EnemyKnock knock;
        [SerializeField] private EnemyMove move;
        [SerializeField] private EnemyAttack attack;

        //パラメータ
        [SerializeField] private EnemyParameter _parameter;
        public EnemyParameter parameter => _parameter;


        //イベント
        private Subject<Unit> _onMove;
        public IObservable<Unit> onMove => _onMove;
        private Subject<Unit> _onAttack;
        public IObservable<Unit> onAttack => _onAttack;
        private Subject<EnemyDamageEvent> _onDamage;
        public IObservable<EnemyDamageEvent> onDamage => _onDamage;



        //ダメージ
        public void Damage(float damage, float knock, float hate, GameObject cause)
        {
            //ダメージ
            float damaged = health.Damage(damage);
            _onDamage.OnNext(new EnemyDamageEvent(health.maxHealth, health.currentHealth, damage));

            //ノックバック
            float knocked = this.knock.KnockBack(transform.position - cause.transform.position, knock);

            //ヘイト値
            this.hate.AddHate(hate, cause);
        }

        //移動
        public bool MoveToTarget()
        {
            _onMove.OnNext(Unit.Default);
            return move.MoveToTarget(hate.GetMaxHateObject().transform.position);
        }


        //攻撃 攻撃動作にかかる時間を返す
        public float Attack(int index)
        {
            _onAttack.OnNext(Unit.Default);
            return attack.AttackToTarget(hate.GetMaxHateObject(), index);
        }



        //初期化
        public void Initialize(int level)
        {
            //パラメータ初期化
            parameter.Initialize(level);
        }



        //イベントの初期化は別途自動で行う
        private void OnEnable()
        {
            //イベント初期化
            _onMove = new Subject<Unit>();
            _onAttack = new Subject<Unit>();
            _onDamage = new Subject<EnemyDamageEvent>();
        }
        private void OnDisable()
        {
            _onMove.Dispose();
            _onAttack.Dispose();
            _onDamage.Dispose();
        }
    }
}

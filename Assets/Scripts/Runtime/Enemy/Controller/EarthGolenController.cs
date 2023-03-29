using UnityEngine;

using StateMachines;

using Runtime.Enemy.Component.Attack;
using Runtime.Enemy.State;
using Runtime.Enemy.State.Taunt;    //ほんとはダメ


namespace Runtime.Enemy.Controller
{
    public class EarthGolenController : EnemyController
    {
        //ステートマシンのセットアップ
        private void Awake()
        {
            //一番外側のステートマシン生成
            stateMachine = new StateMachine<EnemyController>(this, blackBoard);


            /*
             * targetTowerStateの構築
             */
            TargetTowerState targetTower = new TargetTowerState(this, blackBoard);
            TauntIdleState idleTower = new TauntIdleState(this, blackBoard);
            SeekStateByComponent seekTower = new SeekStateByComponent(this, blackBoard);
            SeekFailedState failedTower = new SeekFailedState(this, blackBoard);
            AttackState attackTower = new AttackState(this, blackBoard);

            //変数初期化
            targetTower.isResume = false;
            idleTower.seekRange = 5.2f;
            idleTower.attackCoolTime = 0.7f;
            /*
            seekTower.minVelocity = 0.1f;
            seekTower.maxAgentDistance = 1.0f;
            seekTower.followAgentDistance = 0.25f;
            seekTower.isFixedAgentDistance = false;
            */
            seekTower.stoppingDistance = 5.0f;
            attackTower.index = (int)TauntAttackType.Melee;

            //遷移条件の追加
            targetTower.innerStateMachine.AddTransition(idleTower, seekTower, blackBoard => { return blackBoard.GetValue<bool>("Seek"); });
            targetTower.innerStateMachine.AddTransition(idleTower, attackTower, blackBoard => { return blackBoard.GetValue<bool>("Attack"); });
            targetTower.innerStateMachine.AddTransition(seekTower, failedTower, blackBoard => { return blackBoard.GetValue<bool>("SeekFailed"); });
            targetTower.innerStateMachine.AddTransition(seekTower, idleTower, blackBoard => { return !blackBoard.GetValue<bool>("Seek"); });
            targetTower.innerStateMachine.AddTransition(attackTower, idleTower, blackBoard => { return !blackBoard.GetValue<bool>("Attack"); });

            //ブラックボードのパラメータを初期化
            blackBoard.SetValue<bool>("Seek", false);
            blackBoard.SetValue<bool>("Attack", false);
            blackBoard.SetValue<bool>("SeekFailed", false);

            //ステートマシンの初期化
            targetTower.innerStateMachine.Initialize(idleTower);



            /*
             * targetPlayerStateの構築
             */
            TargetPlayerState targetPlayer = new TargetPlayerState(this, blackBoard);
            TauntIdleState idlePlayer = new TauntIdleState(this, blackBoard);
            SeekStateByComponent seekPlayer = new SeekStateByComponent(this, blackBoard);
            SeekFailedState failedPlayer = new SeekFailedState(this, blackBoard);
            AttackState attackPlayer = new AttackState(this, blackBoard);

            //変数初期化
            targetPlayer.isResume = false;
            idlePlayer.seekRange = 6.3f;
            idlePlayer.attackCoolTime = 1.0f;
            /*
            seekPlayer.minVelocity = 0.1f;
            seekPlayer.maxAgentDistance = 1.0f;
            seekPlayer.followAgentDistance = 0.25f;
            */
            seekPlayer.stoppingDistance = 6.0f;
            attackPlayer.index = (int)TauntAttackType.Melee;

            //遷移条件の追加
            targetPlayer.innerStateMachine.AddTransition(idlePlayer, seekPlayer, blackBoard => { return blackBoard.GetValue<bool>("Seek"); });
            targetPlayer.innerStateMachine.AddTransition(idlePlayer, attackPlayer, blackBoard => { return blackBoard.GetValue<bool>("Attack"); });
            targetPlayer.innerStateMachine.AddTransition(seekPlayer, failedPlayer, blackBoard => { return blackBoard.GetValue<bool>("SeekFailed"); });
            targetPlayer.innerStateMachine.AddTransition(seekPlayer, idlePlayer, blackBoard => { return !blackBoard.GetValue<bool>("Seek"); });
            targetPlayer.innerStateMachine.AddTransition(failedPlayer, idlePlayer, blackBoard => { return !blackBoard.GetValue<bool>("SeekFailed"); });
            targetPlayer.innerStateMachine.AddTransition(attackPlayer, idlePlayer, blackBoard => { return !blackBoard.GetValue<bool>("Attack"); });

            //ステートマシンの初期化
            targetPlayer.innerStateMachine.Initialize(idlePlayer);



            /*
             * ActionStateの構築
             */
            ActionState action = new ActionState(this, blackBoard);

            action.isResume = false;

            //遷移の追加
            action.innerStateMachine.AddTransition(targetTower, targetPlayer, blackBoard => { return blackBoard.GetValue<bool>("Player"); });
            action.innerStateMachine.AddTransition(targetPlayer, targetTower, blackBoard => { return !blackBoard.GetValue<bool>("Player"); });

            //ブラックボードのパラメータを初期化
            blackBoard.SetValue<bool>("Player", false);

            //ステートマシンの初期化
            action.innerStateMachine.Initialize(targetTower);



            /*
             * stateMachineの構築
             */
            TauntSpawnState spawn = new TauntSpawnState(this, blackBoard);
            DamagedState damaged = new DamagedState(this, blackBoard);
            DeathState death = new DeathState(this, blackBoard);


            //遷移の追加
            stateMachine.AddTransition(spawn, action, blackBoard => { return !blackBoard.GetValue<bool>("Spawn"); });
            stateMachine.AddTransition(action, death, blackBoard => { return blackBoard.GetValue<bool>("Death"); });
            stateMachine.AddTransition(damaged, death, blackBoard => { return blackBoard.GetValue<bool>("Death"); });
            stateMachine.AddTransition(damaged, damaged, blackBoard => { return blackBoard.GetValue<Vector3>("Damaged") != Vector3.zero; });
            stateMachine.AddTransition(action, damaged, blackBoard => { return blackBoard.GetValue<Vector3>("Damaged") != Vector3.zero; });
            stateMachine.AddTransition(damaged, action, blackBoard => { return blackBoard.GetValue<bool>("DamagedEnd"); });

            //ブラックボードのパラメータを初期化
            blackBoard.SetValue<bool>("Spawn", true);
            blackBoard.SetValue<bool>("DamagedEnd", false);
            blackBoard.SetValue<Vector3>("Damaged", Vector3.zero);
            blackBoard.SetValue<bool>("Death", false);

            //ステートマシンの初期化
            stateMachine.Initialize(spawn);
        }




        private new void OnEnable()
        {
            base.OnEnable();

            //ヘイト初期化
            GameObject player = GameObject.Find("Player");
            GameObject tower = GameObject.Find("Tower");
            hate.AddHate(0, player);
            hate.AddHate(5, tower);
        }
    }
}


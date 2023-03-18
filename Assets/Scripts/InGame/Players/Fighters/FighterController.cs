using Cysharp.Threading.Tasks;
using InGame.Cameras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Players.Fighters
{
    public class FighterController : PlayerController
    {
        private FighterAttacker fighterAttacker;

        [Inject]
        public FighterController(CameraManager cameraManager, PlayerManager playerManager) : base(cameraManager, playerManager)
        {
            Debug.Log("Create Fighter Controller");
        }

        public override void StartControll(GameObject playerObject)
        {
            fighterAttacker = playerObject.GetComponent<FighterAttacker>();

            base.StartControll(playerObject);

            ControllPlayerAttackAsync(tokenSource.Token).Forget();
        }

        protected override async UniTask ControllPlayerAttackAsync(CancellationToken token)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => playerInput.IsPushingNormalAttack, cancellationToken: token);
                if (token.IsCancellationRequested)
                    break;
                fighterAttacker.NormalAttack();
                var interval = playerManager.playerParameter.AttackInterval * 0.95f;
                await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);
            }
        }
    }
}


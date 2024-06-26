using Cysharp.Threading.Tasks;
using InGame.Cameras;
using InGame.Targets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Players.Archers
{
    public class ArcherController : PlayerController
    {
        private ArcherAttacker archerAttacker;
        private readonly TargetManager targetManager;

        [Inject]
        public ArcherController(CameraManager cameraManager, TargetManager targetManager, PlayerManager playerManager) : base(cameraManager, playerManager)
        {
            Debug.Log("Create Archer Controller");

            this.targetManager = targetManager;
        }

        public override void StartControll(GameObject playerObject)
        {
            archerAttacker = playerObject.GetComponent<ArcherAttacker>();
            playerObject.GetComponent<ArcherEffectPlayer>().Init(targetManager);

            base.StartControll(playerObject);
        }

        protected override async UniTask ControllPlayerAttackAsync(CancellationToken token)
        {
            while (true)
            {
                var result = await UniTask.WhenAny(
                    UniTask.WaitUntil(() => playerInput.IsPushingNormalAttack, cancellationToken: token),
                    UniTask.WaitUntil(() => playerInput.HadPushedSpecialAttack, cancellationToken: token)
                    );

                if (token.IsCancellationRequested)
                    break;

                if (targetManager.TargetedTransform.Value == null)
                {
                    await UniTask.DelayFrame(1, cancellationToken: token);
                    continue;
                }
                
                currentControlledPlayerObj.transform.LookAt(targetManager.TargetedTransform.Value.position);
                if (result == 0)
                {
                    await archerAttacker.NormalAttack(targetManager.currentTargetEnemy);
                }
                else if (result == 1)
                {
                    await archerAttacker.SpecialAttack(targetManager.currentTargetEnemy);
                }
                await UniTask.DelayFrame(1, cancellationToken: token);
            }
        }
    }
}


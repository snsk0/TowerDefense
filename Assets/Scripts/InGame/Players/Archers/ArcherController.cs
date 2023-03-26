using Cysharp.Threading.Tasks;
using InGame.Cameras;
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
        private readonly TargetSearcher targetSearcher;

        [Inject]
        public ArcherController(CameraManager cameraManager, TargetSearcher targetSearcher, PlayerManager playerManager) : base(cameraManager, playerManager)
        {
            Debug.Log("Create Archer Controller");

            this.targetSearcher = targetSearcher;
        }

        public override void StartControll(GameObject playerObject)
        {
            archerAttacker = playerObject.GetComponent<ArcherAttacker>();

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

                if (result == 0)
                {
                    var target = targetSearcher.SerchTarget(currentControlledPlayerObj.transform.position);
                    await archerAttacker.NormalAttack(target, token);
                    await UniTask.DelayFrame(1, cancellationToken: token);
                }
                else if (result == 1)
                {
                    var target = targetSearcher.SerchTarget(currentControlledPlayerObj.transform.position);
                    await archerAttacker.SpecialAttack(target, token);
                    await UniTask.DelayFrame(1, cancellationToken: token);
                }
                
            }
        }
    }
}


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
        private TargetSearcher targetSearcher;

        [Inject]
        public ArcherController(CameraManager cameraManager, TargetSearcher targetSearcher) : base(cameraManager)
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
                await UniTask.WaitUntil(() => playerInput.IsPushingNormalAttack, cancellationToken: token);
                if (token.IsCancellationRequested)
                    break;
                var target = targetSearcher.SerchTarget(currentControlledPlayerObj.transform.position);
                archerAttacker.Attack(target);
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken:token);
            }
        }
    }
}


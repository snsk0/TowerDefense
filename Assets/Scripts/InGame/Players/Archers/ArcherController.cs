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

        [Inject]
        public ArcherController(CameraManager cameraManager) : base(cameraManager)
        {
            Debug.Log("Create Archer Controller");
        }

        public override void StartControll(GameObject playerObject)
        {
            archerAttacker = playerObject.GetComponent<ArcherAttacker>();

            base.StartControll(playerObject);
        }

        protected override async UniTask ControllPlayerAttack(CancellationToken token)
        {
            while (true)
            {
                await UniTask.WaitUntil(() => playerInput.IsPushingNormalAttack, cancellationToken: token);
                if (token.IsCancellationRequested)
                    break;
                archerAttacker.Attack();
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken:token);
            }
        }
    }
}


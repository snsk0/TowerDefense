using Cysharp.Threading.Tasks;
using InGame.Cameras;
using InGame.Players.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Players
{
    public class PlayerController : ControllerBase, IDisposable
    {
        protected readonly CameraManager cameraManager;
        protected readonly PlayerManager playerManager;
        protected readonly PlayerInput playerInput = new PlayerInput();

        protected GameObject currentControlledPlayerObj;
        
        protected PlayerMover playerMover;
        private PlayerJumper playerJumper;
        //protected PlayerAttacker playerAttacker;

        protected CancellationTokenSource tokenSource;

        [Inject]
        public PlayerController(CameraManager cameraManager, PlayerManager playerManager)
        {
            this.cameraManager = cameraManager;
            this.playerManager = playerManager;
        }

        public virtual void StartControll(GameObject playerObject)
        {
            currentControlledPlayerObj = playerObject;

            playerMover = playerObject.GetComponent<PlayerMover>();
            playerJumper = playerObject.GetComponent<PlayerJumper>();

            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            ControllPlayerMovement();
            ControllPlayerAttackAsync(tokenSource.Token).Forget();
        }

        private void ControllPlayerMovement()
        {
            MovePlayerAsync(tokenSource.Token).Forget();

            this.ObserveEveryValueChanged(x => x.playerInput.HadPushedJump)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    playerJumper.StartJump();
                })
                .AddTo(this);

            this.ObserveEveryValueChanged(x => x.playerInput.HadPushedAvoid)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    var sprintVec = cameraManager.mainCameraTransform.TransformDirection(playerInput.MoveVec).normalized;
                    sprintVec.y = 0;
                    playerMover.Sprint(sprintVec);
                })
                .AddTo(this);

        }

        protected virtual async UniTask ControllPlayerAttackAsync(CancellationToken token)
        {

        }

        protected async UniTask MovePlayerAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                    return;

                if (cameraManager.mainCameraTransform == null)
                    return;

                //カメラの方向に適した移動方向を計算
                var moveVec = cameraManager.mainCameraTransform.TransformDirection(playerInput.MoveVec).normalized;
                moveVec.y = 0;

                playerMover?.Move(moveVec);
                await UniTask.DelayFrame(1, cancellationToken:token);
            }
        }

        public void Dispose()
        {
            tokenSource.Cancel();
        }
    }
}


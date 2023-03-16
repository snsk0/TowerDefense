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
        private readonly CameraManager cameraManager;
        protected readonly PlayerInput playerInput = new PlayerInput();

        protected GameObject currentControlledPlayerObj;
        private PlayerMover playerMover;
        private PlayerJumper playerJumper;
        private PlayerAvoider playerAvoider;
        //protected PlayerAttacker playerAttacker;

        private CancellationTokenSource tokenSource;

        [Inject]
        public PlayerController(CameraManager cameraManager)
        {
            this.cameraManager = cameraManager;
        }

        public virtual void StartControll(GameObject playerObject)
        {
            currentControlledPlayerObj = playerObject;

            playerMover = playerObject.GetComponent<PlayerMover>();
            playerJumper = playerObject.GetComponent<PlayerJumper>();
            playerAvoider = playerObject.GetComponent<PlayerAvoider>();
            //playerAttacker = playerObject.GetComponent<PlayerAttacker>();

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
                    playerAvoider.Avoid();
                })
                .AddTo(this);

            //this.ObserveEveryValueChanged(x => x.playerInput.HadPushedAttack)
            //    .Where(x => x)
            //    .Subscribe(_ =>
            //    {
            //        playerAttacker.Attack();
            //    })
            //    .AddTo(this);
        }

        protected virtual async UniTask ControllPlayerAttackAsync(CancellationToken token)
        {

        }

        private async UniTask MovePlayerAsync(CancellationToken token)
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


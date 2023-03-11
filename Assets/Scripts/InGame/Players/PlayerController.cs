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
        private readonly PlayerInput playerInput = new PlayerInput();

        private PlayerMover playerMover;
        private PlayerJumper playerJumper;
        private PlayerAvoider playerAvoider;
        private PlayerAttacker playerAttacker;

        private CancellationTokenSource tokenSource;

        [Inject]
        public PlayerController(CameraManager cameraManager)
        {
            this.cameraManager = cameraManager;
        }

        public void StartControll(GameObject playerObject)
        {
            playerMover = playerObject.GetComponent<PlayerMover>();
            playerJumper = playerObject.GetComponent<PlayerJumper>();
            playerAvoider = playerObject.GetComponent<PlayerAvoider>();
            playerAttacker = playerObject.GetComponent<PlayerAttacker>();

            ControllPlayer();
        }

        private void ControllPlayer()
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

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

            this.ObserveEveryValueChanged(x => x.playerInput.HadPushedAttack)
                .Where(x => x)
                .Subscribe(_ =>
                {
                    playerAttacker.Attack();
                })
                .AddTo(this);
        }

        private async UniTask MovePlayerAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                    return;

                //ƒJƒƒ‰‚Ì•ûŒü‚É“K‚µ‚½ˆÚ“®•ûŒü‚ğŒvZ
                var moveVec = cameraManager.mainCameraTransform.TransformDirection(playerInput.MoveVec).normalized;
                moveVec.y = 0;

                playerMover?.Move(moveVec);
                await UniTask.DelayFrame(1);
            }
        }

        public void Dispose()
        {
            tokenSource.Cancel();
        }
    }
}


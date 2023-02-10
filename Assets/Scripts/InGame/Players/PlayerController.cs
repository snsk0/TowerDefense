using Cysharp.Threading.Tasks;
using InGame.Players.Input;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerController : ControllerBase
    {
        private PlayerInput playerInput = new PlayerInput();

        private PlayerMover playerMover;
        private PlayerJumper playerJumper;

        private CancellationTokenSource tokenSource;

        public void StartControll(GameObject playerObject)
        {
            playerMover = playerObject.GetComponent<PlayerMover>();
            playerJumper = playerObject.GetComponent<PlayerJumper>();

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
                    playerJumper.Jump();
                })
                .AddTo(this);
        }

        private async UniTask MovePlayerAsync(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                    return;

                playerMover.Move(playerInput.MoveVec);
                await UniTask.DelayFrame(1);
            }
        }
    }
}


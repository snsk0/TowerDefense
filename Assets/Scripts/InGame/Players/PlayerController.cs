using InGame.Players.Input;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerController : ControllerBase
    {
        private PlayerInput playerInput = new PlayerInput();

        private PlayerMover playerMover;

        public void StartControll(GameObject playerObject)
        {
            playerMover = playerObject.GetComponent<PlayerMover>();

            ControllPlayer();
        }

        private void ControllPlayer()
        {
            this.ObserveEveryValueChanged(x => x.playerInput.MoveVec)
                .Subscribe(vec =>
                {
                    playerMover.Move(vec);
                })
                .AddTo(this);
        }
    }
}


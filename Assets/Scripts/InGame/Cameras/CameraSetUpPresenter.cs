using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace InGame.Cameras
{
    public class CameraSetUpPresenter : ControllerBase, IInitializable
    {
        private readonly PlayerManager playerManager;
        private readonly CameraManager cameraManager;

        [Inject]
        public CameraSetUpPresenter(PlayerManager playerManager, CameraManager cameraManager)
        {
            this.playerManager = playerManager;
            this.cameraManager = cameraManager;
        }

        public void Initialize()
        {
            playerManager.GeneratePlayerObservable
                .Subscribe(player =>
                {
                    cameraManager.SetTarget(player.transform);
                })
                .AddTo(this);
        }
    }
}


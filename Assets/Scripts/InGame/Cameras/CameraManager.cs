using Cinemachine;
using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace InGame.Cameras
{
    public class CameraManager : ControllerBase
    {
        private readonly CinemachineVirtualCamera virtualCamera;

        public readonly Transform mainCameraTransform;

        [Inject]
        public CameraManager(CinemachineVirtualCamera virtualCamera)
        {
            this.virtualCamera = virtualCamera;

            mainCameraTransform = Camera.main.transform;
        }

        public void SetTarget(Transform targetTransform)
        {
            virtualCamera.Follow = targetTransform;
            virtualCamera.LookAt = targetTransform;
        }
    }
}


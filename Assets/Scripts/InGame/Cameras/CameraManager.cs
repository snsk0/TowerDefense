using Cinemachine;
using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace InGame.Cameras
{
    public class CameraManager : ControllerBase
    {
        private readonly Camera mainCamera;
        private readonly CinemachineVirtualCamera virtualCamera;

        private readonly Transform mainCameraTransform;

        public Vector3 CameraForwardXZ => new Vector3(mainCameraTransform.forward.x, 0, mainCameraTransform.forward.z).normalized;

        [Inject]
        public CameraManager(Camera mainCamera, CinemachineVirtualCamera virtualCamera)
        {
            this.mainCamera = mainCamera;
            this.virtualCamera = virtualCamera;

            mainCameraTransform = mainCamera.transform;
        }

        public void SetTarget(Transform targetTransform)
        {
            virtualCamera.Follow = targetTransform;
            virtualCamera.LookAt = targetTransform;
        }
    }
}


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
        private readonly CinemachineFreeLook freeLookCamera;

        public readonly Transform mainCameraTransform;

        [Inject]
        public CameraManager(CinemachineFreeLook freeLookCamera)
        {
            this.freeLookCamera = freeLookCamera;

            mainCameraTransform = Camera.main.transform;
        }

        public void SetTarget(Transform targetTransform)
        {
            freeLookCamera.Follow = targetTransform;
            freeLookCamera.LookAt = targetTransform.GetChild("Head");
        }
    }
}


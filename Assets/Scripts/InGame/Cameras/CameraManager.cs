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
        public readonly CinemachineFreeLook freeLookCamera;
        public readonly Camera mainCamera;
        public readonly Transform mainCameraTransform;

        [Inject]
        public CameraManager(CinemachineFreeLook freeLookCamera)
        {
            this.freeLookCamera = freeLookCamera;

            mainCamera = Camera.main;
            mainCameraTransform = mainCamera.transform;
        }

        public void SetTarget(Transform targetTransform)
        {
            freeLookCamera.Follow = targetTransform;
            freeLookCamera.LookAt = targetTransform;

            freeLookCamera.m_YAxis.Value = 0.5f;
        }

        public void SetSensitivity(float xValue, float yValue)
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = xValue;
            freeLookCamera.m_YAxis.m_MaxSpeed = yValue;
        }

        public void SetInvert(bool xValue, bool yValue)
        {
            freeLookCamera.m_XAxis.m_InvertInput = xValue;
            freeLookCamera.m_YAxis.m_InvertInput = yValue;
        }
    }
}


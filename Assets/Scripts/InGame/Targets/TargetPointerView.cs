using Cysharp.Threading.Tasks;
using InGame.Cameras;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;

namespace InGame.Targets
{
    public class TargetPointerView : MonoBehaviour
    {
        [SerializeField] private GameObject targetPointerObject;
        [Inject] private CameraManager cameraManager;

        private Transform targetTransform;
        private CancellationTokenSource tokenSource;

        public void SetTargetTransform(Transform targetTransform)
        {
            this.targetTransform = targetTransform;

            tokenSource?.Cancel();
            if (targetTransform == null)
            {
                targetPointerObject.SetActive(false);
                return;
            }

            targetPointerObject.SetActive(true);
            tokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy(), new CancellationToken());
            UpdatePointerPosition(tokenSource.Token).Forget();
        }

        private async UniTaskVoid UpdatePointerPosition(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                //ターゲットの位置にポインターを表示させ、カメラの方向を向かせる
                var position = targetTransform.position - cameraManager.mainCameraTransform.forward;
                targetPointerObject.transform.position = position;
                targetPointerObject.transform.LookAt(targetPointerObject.transform.position - cameraManager.mainCameraTransform.forward);
                await UniTask.DelayFrame(1, cancellationToken: token);
            }
        }
    }
}


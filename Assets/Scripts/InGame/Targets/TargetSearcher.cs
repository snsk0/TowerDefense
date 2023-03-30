using InGame.Cameras;
using InGame.Players;
using Runtime.Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace InGame.Targets
{
    public class TargetSearcher
    {
        private readonly PlayerManager playerManager;
        private readonly EnemyManager enemyManager;
        private readonly CameraManager cameraManager;

        private readonly Rect searchArea = new Rect(0.2f, 0.3f, 0.6f, 0.5f);

        [Inject]
        public TargetSearcher(PlayerManager playerManager, EnemyManager enemyManager, CameraManager cameraManager)
        {
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.cameraManager = cameraManager;
        }

        public Transform SerchTarget()
        {
            if (playerManager.currentPlayerObject == null)
                return null;

            var playerPos = playerManager.currentPlayerObject.transform.position;
            var cameraPos = cameraManager.mainCamera.transform.position;
            var cameraToPlayerVec = Vector3Calculator.MulElements((playerPos - cameraPos), new Vector3(1, 0, 1));

            //プレイヤーに近い敵のうち、カメラから見て奥側にいて、画面中央に最も近い敵の取得
            var target = enemyManager.LivingEnemyTransformList
                                    .Select(x => x.transform)
                                    .Where(x => Vector3.Distance(x.position, playerPos) < 10f)
                                    .Where(x => Vector3.Dot(Vector3Calculator.MulElements((x.position - playerPos), new Vector3(1, 0, 1)), cameraToPlayerVec) < 90)
                                    .Select(x => (x, cameraManager.mainCamera.WorldToViewportPoint(x.position)))
                                    .Where(t => searchArea.xMin <= t.Item2.x && t.Item2.x <= searchArea.xMax && searchArea.yMin <= t.Item2.y && t.Item2.y <= searchArea.yMax)
                                    .OrderBy(t => Vector2.Distance(t.Item2, Vector2.zero))
                                    .FirstOrDefault().Item1;

            return target;
        }
    }
}


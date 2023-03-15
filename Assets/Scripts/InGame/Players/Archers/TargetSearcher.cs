using InGame.Cameras;
using InGame.Enemies;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace InGame.Players.Archers
{
    public class TargetSearcher
    {
        private EnemyManager enemyManager;
        private CameraManager cameraManager;

        [Inject]
        public TargetSearcher(EnemyManager enemyManager, CameraManager cameraManager)
        {
            this.enemyManager = enemyManager;
            this.cameraManager = cameraManager;
        }

        public IEnemyDamagable SerchTarget(Vector3 playerPos)
            => enemyManager.CurrentEnemyObjects
                .Where(x => Vector3.Distance(x.transform.position, playerPos) < 10f)
                .OrderBy(x => Vector2.Distance(cameraManager.mainCamera.WorldToViewportPoint(x.transform.position), Vector2.zero))
                .Take(1)
                .Select(x => x.GetComponent<IEnemyDamagable>())
                .Single();
    }
}


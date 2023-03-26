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
        private readonly EnemyManager enemyManager;
        private readonly CameraManager cameraManager;

        private readonly Rect searchArea = new Rect(0.2f, 0.3f, 0.6f, 0.5f);

        [Inject]
        public TargetSearcher(EnemyManager enemyManager, CameraManager cameraManager)
        {
            this.enemyManager = enemyManager;
            this.cameraManager = cameraManager;
        }

        public IEnemyDamagable SerchTarget(Vector3 playerPos)
            => enemyManager.CurrentEnemyObjects
                .Where(x => Vector3.Distance(x.transform.position, playerPos) < 10f)
                .Select(x => (x, cameraManager.mainCamera.WorldToViewportPoint(x.transform.position)))
                .Where(t => searchArea.xMin <= t.Item2.x && t.Item2.x <= searchArea.xMax && searchArea.yMin <= t.Item2.y && t.Item2.y <= searchArea.yMax)
                .OrderBy(t => Vector2.Distance(t.Item2, Vector2.zero))
                .Take(1)
                .Select(t => t.Item1.GetComponent<IEnemyDamagable>())
                .FirstOrDefault();
    }
}


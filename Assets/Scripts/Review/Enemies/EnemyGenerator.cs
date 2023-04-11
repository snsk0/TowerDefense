using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Review.Enemies
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject slimePrefab;
        [SerializeField] private GameObject goblinPrefab;
        [SerializeField] private GameObject golemPrefab;

        public GameObject GenerateEnemy(EnemyType enemyType)
        {
            var prefab = GetPrefab(enemyType);
            if (prefab == null)
                return null;
            var enemy=Instantiate(prefab);
            return enemy;
        }

        private GameObject GetPrefab(EnemyType enemyType)
        {
            var prefab= enemyType switch
            {
                EnemyType.None => null,
                EnemyType.Slime => slimePrefab,
                EnemyType.Goblin => golemPrefab,
                EnemyType.Golem => golemPrefab,
                _ => throw new System.NotImplementedException(),
            };
            return prefab;
        }
    }
}


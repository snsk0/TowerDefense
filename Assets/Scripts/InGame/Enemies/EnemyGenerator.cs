using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Enemies
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;

        public GameObject GenerateEnemy()
        {
            var enemy = Instantiate(enemyPrefab, new Vector3(0,0.5f,1f), Quaternion.identity);
            return enemy;
        }
    }
}


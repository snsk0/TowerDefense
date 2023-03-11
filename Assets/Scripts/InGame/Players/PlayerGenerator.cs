using InGame.Players.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerGenerator : MonoBehaviour
    {
        [SerializeField] private PlayerPrefabTable playerPrefabTable;

        public GameObject GeneratePlayer(PlayerCharacterType playerCharacterType)
        {
            var prefab = playerPrefabTable.GetPlayerPrefab(playerCharacterType);
            var player = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            return player;
        }
    }
}


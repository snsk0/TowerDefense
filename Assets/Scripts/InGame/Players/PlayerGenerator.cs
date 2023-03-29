using InGame.Players.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerGenerator : MonoBehaviour
    {
        [SerializeField] private PlayerPrefabTable playerPrefabTable;
        [SerializeField] private Transform generatePosition;

        public GameObject GeneratePlayer(PlayerCharacterType playerCharacterType)
        {
            var prefab = playerPrefabTable.GetPlayerPrefab(playerCharacterType);
            var player = Instantiate(prefab, generatePosition.position, Quaternion.identity);
            return player;
        }
    }
}


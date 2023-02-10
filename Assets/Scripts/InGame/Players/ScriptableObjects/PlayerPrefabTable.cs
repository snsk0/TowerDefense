using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGame.Players.ScriptableObjects
{
    [CreateAssetMenu (menuName ="MyScriptable/PlayerPrefabTable", fileName ="PlayerPrefabTable")]
    public class PlayerPrefabTable : ScriptableObject
    {
        [Serializable]
        private class PlayerPrefabData
        {
            [SerializeField] private PlayerCharacterType playerCharacterType;
            [SerializeField] private GameObject playerPrefab;

            public PlayerCharacterType PlayerCharacterType => playerCharacterType;
            public GameObject PlayerPrefab => playerPrefab;
        }

        [SerializeField] private List<PlayerPrefabData> playerPrefabDataList;

        public GameObject GetPlayerPrefab(PlayerCharacterType playerCharacterType) 
            => playerPrefabDataList.Single(x => x.PlayerCharacterType == playerCharacterType).PlayerPrefab;
    }
}


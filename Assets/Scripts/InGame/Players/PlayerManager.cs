using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace InGame.Players
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerGenerator playerGenerator;
        
        public GameObject currentPlayerObject { get; private set; }

        [Inject]
        public PlayerManager(PlayerGenerator playerGenerator)
        {
            this.playerGenerator = playerGenerator;
        }

        public void GeneratePlayer(PlayerCharacterType playerCharacterType)
        {
            currentPlayerObject = playerGenerator.GeneratePlayer(playerCharacterType);
        }
    }
}


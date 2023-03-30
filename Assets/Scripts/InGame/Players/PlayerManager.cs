using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InGame.Players
{
    public class PlayerManager
    {
        private readonly PlayerGenerator playerGenerator;

        private readonly ISubject<GameObject> generatedPlayerSubject = new Subject<GameObject>();
        public IObservable<GameObject> GeneratePlayerObservable => generatedPlayerSubject;
        
        public GameObject currentPlayerObject { get; private set; }
        public PlayerParameter playerParameter { get; private set; }

        private PlayerHealth playerHealth;
        public bool IsDead => playerHealth.currentHP <= 0;

        [Inject]
        public PlayerManager(PlayerGenerator playerGenerator)
        {
            this.playerGenerator = playerGenerator;
        }

        public void GeneratePlayer(PlayerCharacterType playerCharacterType)
        {
            currentPlayerObject = playerGenerator.GeneratePlayer(playerCharacterType);
            InitPlayer(playerCharacterType);
        }

        private void InitPlayer(PlayerCharacterType playerCharacterType)
        {
            playerParameter = new PlayerParameter(playerCharacterType, InitComponent);
        }

        private void InitComponent()
        {
            currentPlayerObject.GetComponent<PlayerAttacker>().Init(playerParameter);
            currentPlayerObject.GetComponent<PlayerAnimationPlayer>().Init(playerParameter);
            currentPlayerObject.GetComponent<PlayerMover>().Init(playerParameter);
            playerHealth = currentPlayerObject.GetComponent<PlayerHealth>();
            playerHealth.Init(playerParameter);

            generatedPlayerSubject.OnNext(currentPlayerObject);
        }
    }
}
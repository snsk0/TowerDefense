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

        [Inject]
        public PlayerManager(PlayerGenerator playerGenerator)
        {
            this.playerGenerator = playerGenerator;
        }

        public void GeneratePlayer(PlayerCharacterType playerCharacterType)
        {
            currentPlayerObject = playerGenerator.GeneratePlayer(playerCharacterType);
            generatedPlayerSubject.OnNext(currentPlayerObject);

            playerParameter = new PlayerParameter(playerCharacterType);
            currentPlayerObject.GetComponent<PlayerAttacker>().Init(playerParameter);
            currentPlayerObject.GetComponent<PlayerAnimationPlayer>().Init(playerParameter);
            currentPlayerObject.GetComponent<PlayerAvoider>().Init(playerParameter);
            currentPlayerObject.GetComponent<PlayerMover>().Init(playerParameter);
        }
    }
}
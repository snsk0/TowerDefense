using InGame.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Prepare
{
    public class CharacterSelectView : MonoBehaviour
    {
        [SerializeField] private Button fighterButton;
        [SerializeField] private Button archerButton;
        [SerializeField] private Button startButton;

        private readonly ISubject<PlayerCharacterType> selectedPlayerCharacterTypeSubject = new Subject<PlayerCharacterType>();
        public IObservable<PlayerCharacterType> SelectedPlayerCharacterTypeObservable => selectedPlayerCharacterTypeSubject;

        private void Start()
        {
            fighterButton.onClick.AddListener(() => selectedPlayerCharacterTypeSubject.OnNext(PlayerCharacterType.Fighter));
            archerButton.onClick.AddListener(() => selectedPlayerCharacterTypeSubject.OnNext(PlayerCharacterType.Archer));

            //startButton.onClick.AddListener(() => SceneManager.LoadScene("TestScene_PlayerControll"));
            startButton.onClick.AddListener(() => SceneManager.LoadScene("WaveSample"));
        }
    }
}


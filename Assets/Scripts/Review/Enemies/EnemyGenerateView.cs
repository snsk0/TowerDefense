using Review.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Review.StateMachines
{
    public class EnemyGenerateView : MonoBehaviour
    {
        [SerializeField] private Button slimeGenerateButton;
        [SerializeField] private Button goblinGenerateButton;
        [SerializeField] private Button golemGenerateButton;

        private Subject<EnemyType> generateTypeSubject = new Subject<EnemyType>();
        public IObservable<EnemyType> GenerateTypeObservable => generateTypeSubject;

        private void Start()
        {
            slimeGenerateButton.onClick.AddListener(() => generateTypeSubject.OnNext(EnemyType.Slime));
            goblinGenerateButton.onClick.AddListener(() => generateTypeSubject.OnNext(EnemyType.Goblin));
            golemGenerateButton.onClick.AddListener(() => generateTypeSubject.OnNext(EnemyType.Golem));
        }
    }
}


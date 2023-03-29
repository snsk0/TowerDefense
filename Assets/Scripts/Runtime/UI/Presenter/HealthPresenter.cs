using System;

using UnityEngine;

using UniRx;

using Runtime.UI.View;
using Runtime.Enemy.Component;

namespace Runtime.UI.Presenter
{
    public class HealthPresenter : MonoBehaviour
    {
        //コンポーネント
        [SerializeField] private EnemyHealth health;
        [SerializeField] private Canvas canvas;
        [SerializeField] private HealthView view;

        //dispose
        private IDisposable disposable;

        //最初に一度だけ登録
        private void Start()
        {
            disposable = health.currentHealth.Subscribe(health =>
            {
                view.SetValue(health / this.health.maxHealth);
            });
        }

        //キャンバスをカメラに向ける
        private void Update()
        {
            canvas.worldCamera = Camera.main;
            canvas.transform.rotation = canvas.worldCamera.transform.rotation;
        }

        //disposeになった時に解除する
        private void OnDisable()
        {
            if (disposable != null) disposable.Dispose();
        }
    }
}

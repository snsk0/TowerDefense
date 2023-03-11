using Cysharp.Threading.Tasks;
using InGame.Players.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerAnimationPlayer : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;

        private PlayerInput playerInput = new PlayerInput();

        public async UniTask PlayAvoidAnimationAsync(CancellationToken token)
        {
            //TODO:アニメーションの実行に書き換える
            var time = 0f;

            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                rigidbody.AddForce(playerInput.MoveVec * 100);
                await UniTask.DelayFrame(1);
                time += Time.deltaTime;
                if (time > 0.3f)
                    break;
            }
        }

        public async virtual UniTask PlayAttackAnimation(CancellationToken token, Action<bool> attackCallback = null)
        {
            //それぞれのキャラの子クラスで実装
        }
    }
}


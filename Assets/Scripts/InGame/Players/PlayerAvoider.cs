using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerAvoider : MonoBehaviour
    {
        [SerializeField] private PlayerDamagable playerDamagable;
        [SerializeField] private PlayerAnimationPlayer playerAnimationPlayer;

        private PlayerParameter playerParameter;

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public async void Avoid()
        {
            playerDamagable.SetDamagable(false);
            await playerAnimationPlayer.PlayAvoidAnimationAsync(this.GetCancellationTokenOnDestroy());
            playerDamagable.SetDamagable(true);
        }
    }
}


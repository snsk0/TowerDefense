using UnityEngine;

using InGame.Players;
using VContainer;

namespace Runtime.Wave
{
    public class PlayerManagerProvider : MonoBehaviour
    {
        [Inject] private readonly PlayerManager _playerManager;
        public PlayerManager playerManager => _playerManager;
    }
}

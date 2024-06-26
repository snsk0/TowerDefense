using InGame.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prepare
{
    //NOTE: シングルトンで管理
    public sealed class PrepareSetting
    {
        public PlayerCharacterType selectedPlayerCharacterType { get; private set; } = PlayerCharacterType.Fighter;

        public void SetSelectedPlayerCharacterType(PlayerCharacterType playerCharacterType)
        {
            selectedPlayerCharacterType = playerCharacterType;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerDamagable : MonoBehaviour, IPlayerDamagable
    {
        public bool IsDamagable { get; private set; }

        public void SetDamagable(bool value)
        {
            IsDamagable = value;
        }
    }
}


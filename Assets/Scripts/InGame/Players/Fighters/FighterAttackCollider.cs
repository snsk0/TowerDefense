using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players.Fighters
{
    public class FighterAttackCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider attackCollider;

        public void EnableCollider(bool value)
        {
            attackCollider.enabled = value;
        }
    }
}
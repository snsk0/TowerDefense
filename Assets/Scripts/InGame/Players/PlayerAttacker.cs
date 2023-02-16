using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerAttacker : MonoBehaviour
    {
        protected PlayerParameter playerParameter;

        public void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public virtual void Attack()
        {
            //ŽqƒNƒ‰ƒX‚ÅŽÀ‘•
        }
    }
}


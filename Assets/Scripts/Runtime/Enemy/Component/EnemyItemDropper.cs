using System;

using UnityEngine;

using Runtime.Enemy.Parameter;

namespace Runtime.Enemy.Component
{
    public class EnemyItemDropper : MonoBehaviour
    {
        //[SerializeField] private ExpOrb dropItem;
        [SerializeField] private Vector3 offset;
        [SerializeField] private EnemyParameter parameter;


        //ŒÄ‚Ño‚·ŠÖ”
        private Action<Vector3, int> generateExp;


        //‰Šú‰»
        public void initialize(Action<Vector3, int> generateExp)
        {
            this.generateExp = generateExp;
        }


        //ŒÄ‚Ño‚µ
        public void Drop()
        {
            generateExp.Invoke(transform.position + offset, (int)parameter.exp);
        }
    }
}

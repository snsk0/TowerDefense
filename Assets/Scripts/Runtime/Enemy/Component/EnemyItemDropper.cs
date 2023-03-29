using UnityEngine;

using Runtime.Enemy.Parameter;

namespace Runtime.Enemy.Component
{
    public class EnemyItemDropper : MonoBehaviour
    {
        [SerializeField] private ExpOrb dropItem;
        [SerializeField] private Vector3 offset;
        [SerializeField] private EnemyParameter parameter;

        public void Drop()
        {
            GameObject go = Instantiate(dropItem.gameObject, transform);
            go.transform.position += offset;

            ExpOrb orb = dropItem.GetComponent<ExpOrb>();
            orb.SetInitialize(parameter);
        }
    }
}

using UnityEngine;

using BehaviorTree;
using Runtime.Enemy.Parameter;


namespace Runtime.Enemy.Component
{
    public class EnemyKnock : MonoBehaviour
    {
        //パラメータ
        [SerializeField] private EnemyParameter parameter;

        [SerializeField] private new Transform transform;

        //BTを中断
        [SerializeField] private SimpleBehaviorTree tree;

        //中断時間
        private float time;
        private float deltTime;
        private Vector3 direction;

        //TODO ノックバック動作の実装
        public float KnockBack(Vector3 direction, float strength)
        {
            if (strength <= 0) return 0;

            this.direction = direction;
            time = parameter.poise * strength;
            deltTime = 0;
            tree.excution = false;

            return parameter.poise * strength;
        }

        private void Update()
        {
            if(time > 0)
            {
                transform.position = transform.position + (direction.normalized * 0.05f);
                deltTime += Time.deltaTime;
                if(deltTime > time)
                {
                    tree.excution = true;
                    time = 0;
                    deltTime = 0;
                }
            }
        }


    }
}

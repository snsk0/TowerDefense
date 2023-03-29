using UnityEngine;

using Runtime.Enemy.Animation;


namespace Runtime.Enemy.Component
{
    public class EnemyLook : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private float step;


        public void Look(Vector3 target)
        {
            //回転Quartanionの計算
            Vector3 direction = target - transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);


            //アニメーション(rotationが一定以上)
            var signedAngle = Vector3.SignedAngle(direction, transform.forward, Vector3.up);
            //Debug.Log(signedAngle);


            //回転操作
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, step);
        }
    }

}
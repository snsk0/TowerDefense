using UnityEngine;

using Runtime.Enemy.Parameter;
using Runtime.Enemy.Animation;

namespace Runtime.Enemy.Component
{
    public class EnemyMove : MonoBehaviour
    {
        //コンポーネント
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private new Collider collider;
        [SerializeField] private PhysicMaterial material;
        [SerializeField] private EnemyAnimator animator;
        [SerializeField] private EnemyParameter parameter;

        //定数
        private const float moveForceMultiplier = 15.0f;

        //Input
        public bool sprint { get; set; } = false;
        private Vector3 direction;

        //デフォルトマテリアル
        private PhysicMaterial defaultMaterial;


        //移動関数
        public void MoveByLocalDir(Vector3 direction)
        {
            //ベクトルを正規して保持
            this.direction = direction.normalized;
        }
        public void MoveByWorldDir(Vector3 direction)
        {
            //逆回転して方向を保持
            direction = Quaternion.Inverse(transform.rotation) * direction;
            this.direction = direction.normalized;
        }



        //移動処理
        public void FixedUpdate()
        {
            //与えられた方向ベクトルを回転して移動ベクトルを求める
            Vector3 vector = transform.rotation * direction;

            //力を加える
            float magnitude = parameter.speed - rigidbody.velocity.magnitude;
            rigidbody.AddForce(moveForceMultiplier * (vector * magnitude - rigidbody.velocity), ForceMode.Acceleration);

            //アニメーションを再生
            animator.PlayRun(Quaternion.Inverse(transform.rotation) * rigidbody.velocity, parameter.speed);

            //入力を初期化
            direction = Vector3.zero;
        }



        //摩擦管理
        private void OnEnable()
        {
            defaultMaterial = collider.material;
            collider.material = material;
            rigidbody.isKinematic = false;
        }
        private void OnDisable()
        {
            collider.material = defaultMaterial;
            rigidbody.isKinematic = true;
        }
    }
}
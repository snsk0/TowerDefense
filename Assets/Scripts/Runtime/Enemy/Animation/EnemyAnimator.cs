using UnityEngine;

using UniRx;


namespace Runtime.Enemy.Animation
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject eventSender;
        private IEnemyEventSender sender;


        [SerializeField] private Animator animator;
 

        //moveƒtƒ‰ƒO
        private bool moved;

        private void Start()
        {
            moved = false;
            sender = eventSender.GetComponent<IEnemyEventSender>();

            sender.onAttack.Subscribe(_ => animator.SetTrigger("Attack")).AddTo(this);
            sender.onMove.Subscribe(_ =>
            {
                animator.SetBool("Move", true);
                moved = true;
            }).AddTo(this);
        }

        private void Update()
        {
            if (moved)
            {
                moved = false;
            }
            else
            {
                animator.SetBool("Move", false);
            }
        }
    }

}
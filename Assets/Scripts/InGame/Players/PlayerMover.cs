using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace InGame.Players
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] protected Rigidbody rigidbody;
        [SerializeField] protected PlayerAnimationPlayer playerAnimationPlayer;
        [SerializeField] private PlayerDamagable playerDamagable;

        protected PlayerParameter playerParameter;

        public virtual void Init(PlayerParameter playerParameter)
        {
            this.playerParameter = playerParameter;
        }

        public virtual void Move(Vector3 moveVec)
        {
            if (rigidbody == null)
                return;

            //if (playerAnimationPlayer.currentBaseState==PlayerBaseStateType.Jump)
            //    return;

            if (playerAnimationPlayer.currentAttackState == PlayerAttackStateType.Special)
                return;

            var moveSpeed = playerParameter.GetCalculatedValue(PlayerParameterType.MoveSpeed);
            var velocity = moveVec * moveSpeed + new Vector3(0, rigidbody.velocity.y, 0);
            rigidbody.velocity = velocity;
            transform.LookAt(transform.position+moveVec);

            if(Mathf.Approximately(velocity.x, 0) && Mathf.Approximately(velocity.z, 0))
            {
                playerAnimationPlayer.StopRunAnimation();
            }
            else
            {
                playerAnimationPlayer.PlayRunAnimation();
            }
        }

        public void Sprint(Vector3 dir)
        {
            playerAnimationPlayer.PlayAvoidAnimation();
            playerDamagable.SetDamagable(false);

            float elaspedTime = 0f;
            float invincibleTime = playerParameter.GetCalculatedValue(PlayerParameterType.InvincibleTime);
            
            //‰ñ”ðŽžŠÔ‚ªŒo‰ß‚·‚é‚Ü‚Å‰ñ”ð•ûŒü‚É—Í‚ð—^‚¦‚é
            this.FixedUpdateAsObservable()
                .TakeWhile(_ => elaspedTime < invincibleTime)
                .Subscribe(_ =>
                {
                    rigidbody.AddForce(dir* playerParameter.GetCalculatedValue(PlayerParameterType.SprintDistance));
                    elaspedTime += Time.deltaTime;
                },()=>playerDamagable.SetDamagable(true))
                .AddTo(this);
        }
    }
}


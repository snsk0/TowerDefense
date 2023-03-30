using System.Collections.Generic;

using UnityEngine;

using UniRx;
using UniRx.Triggers;

using Runtime.Enemy.Parameter;
using InGame.Players;
using InGame.Damages;

namespace Runtime.Enemy.Component.Attack
{
    public class TauntAttack : EnemyAttack
    {
        [SerializeField] private Collider attackCollider;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private EnemyParameter parameter;

        private bool attaked = false;

        private List<GameObject> hitedList = new List<GameObject>();

        
        public override void Attack(int index, GameObject target)
        {
            switch (index)
            {
                case (int)TauntAttackType.Melee:
                    animator.PlayAttack((int)TauntAttackType.Melee);
                    isAttacking = true;
                    attaked = false;
                    hitedList.Clear();
                    break;
            }
        }


        //‰Šú‰»
        private void Awake()
        {
            attackCollider.OnTriggerEnterAsObservable().Subscribe(other => { OnHit(other); }).AddTo(this);
        }

        private void OnHit(Collider other)
        {
            if (!hitedList.Contains(other.gameObject))
            {
                hitedList.Add(other.gameObject);

                IPlayerDamagable damagable = other.GetComponent<IPlayerDamagable>();
                if (!damagable.IsDamagable)
                    return;
                damagable?.ApplyDamage(new Damage(parameter.attack, KnockbackType.Small));

                //if(damagable != null)
                //{
                //    if(!(damagable is EnemyController))
                //    {
                //        damagable.Damage(parameter.attack, 1, this.gameObject);
                //    }
                //}
            }
        }

        private float timer = 0;
        public void Update()
        {
            if (isAttacking)
            {
                timer += Time.deltaTime;
                if (timer > 0.20f && attaked == false)
                {
                    attackCollider.enabled = true;
                    particle.gameObject.SetActive(true);
                    particle.Play(true);
                    attaked = true;
                }
                if (timer > 0.7f)
                {
                    attackCollider.enabled = false;
                    isAttacking = false;
                }
            }

            else
            {
                timer = 0;
            }
        }

    }


    public enum TauntAttackType
    {
        Melee = 0,
        Projectie = 1,
        AreaOfEffect = 2
    }
}

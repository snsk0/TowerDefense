using UnityEngine;

using UniRx;
using UniRx.Triggers;
using InGame.Damages;
using InGame.Players;
using System.Collections.Generic;
using Runtime.Enemy.Parameter;


namespace Runtime.Enemy.Component.Attack
{
    public class GolemAttack : EnemyAttack
    {
        [SerializeField] private Collider attackCollider;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private EnemyParameter parameter;

        
        private bool attaked = false;

        private List<GameObject> hitedList = new List<GameObject>();
        private float timer;
        private Vector3 collidertransform;

        public override void Attack(int index, GameObject target)
        {
            animator.PlayAttack(index);
            collidertransform = attackCollider.transform.position;
            timer = 0;


            isAttacking = true;
            attaked = false;
            hitedList.Clear();
        }


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
                if(timer > 0.20f)
                {
                    Vector3 dir = transform.forward;
                    dir *= 0.25f;
                    attackCollider.transform.position += dir;
                }
                if (timer > 1.5f)
                {
                    attackCollider.transform.position = collidertransform;
                    attackCollider.enabled = false;
                    isAttacking = false;
                }
            }

            else
            {
                timer = 0;
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
                damagable?.ApplyDamage(new Damage(parameter.attack, KnockbackType.None));

                //if(damagable != null)
                //{
                //    if(!(damagable is EnemyController))
                //    {
                //        damagable.Damage(parameter.attack, 1, this.gameObject);
                //    }
                //}
            }
        }
    }
}

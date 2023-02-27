using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;


namespace Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator anime;
        private bool moved = false;

        private void Awake()
        {
            IEnemyEventSender sender = GetComponent<IEnemyEventSender>();

            sender.onAttack.Subscribe(e => anime.SetTrigger("Attack"));
            sender.onMove.Subscribe(e =>
            {
                moved = true;
                anime.SetBool("Move", true);
            });
        }


        private void Update()
        {
            if (moved) moved = false;
            else anime.SetBool("Move", false);
        }
    }

}
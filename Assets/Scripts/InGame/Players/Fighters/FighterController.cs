using InGame.Cameras;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace InGame.Players.Fighters
{
    public class FighterController : PlayerController
    {
        private FighterAttacker fighterAttacker;

        [Inject]
        public FighterController(CameraManager cameraManager) : base(cameraManager)
        {
            Debug.Log("Create Fighter Controller");
        }

        public override void StartControll(GameObject playerObject)
        {
            fighterAttacker = playerObject.GetComponent<FighterAttacker>();

            base.StartControll(playerObject);
        }

        //protected override void ControllPlayerAttack()
        //{
        //    this.ObserveEveryValueChanged(x => x.playerInput.HadPushedAttack)
        //        .Where(x => x)
        //        .Subscribe(_ =>
        //        {
        //            fighterAttacker.Attack();
        //        })
        //        .AddTo(this);
        //}
    }
}


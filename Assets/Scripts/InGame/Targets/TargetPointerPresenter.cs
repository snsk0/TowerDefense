using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace InGame.Targets
{
    public class TargetPointerPresenter : ControllerBase, IStartable
    {
        private readonly TargetManager targetManager;
        private readonly TargetPointerView targetPointerView;

        [Inject]
        public TargetPointerPresenter(TargetManager targetManager, TargetPointerView targetPointerView)
        {
            this.targetManager = targetManager;
            this.targetPointerView = targetPointerView;
        }

        public void Start()
        {
            targetManager.TargetedTransform
                .Skip(1)
                .Subscribe(target =>
                {
                    targetPointerView.SetTargetTransform(target);
                })
                .AddTo(this);
        }
    }
}


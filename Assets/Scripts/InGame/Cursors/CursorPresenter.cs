using InGame.Enhancements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace InGame.Cursors
{
    public class CursorPresenter : ControllerBase, IStartable
    {
        private readonly CursorController cursorController;
        private readonly EnhancementView enhancementView;

        [Inject]
        public CursorPresenter(CursorController cursorController, EnhancementView enhancementView)
        {
            this.cursorController = cursorController;
            this.enhancementView = enhancementView;
        }

        public void Start()
        {
            cursorController.SetVisibleCursoe(false);

            enhancementView.ViewPanelObservable
                .Subscribe(opened =>
                {
                    cursorController.SetVisibleCursoe(opened);
                })
                .AddTo(this);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace Prepare
{
    public class CharacterSelectPresenter : ControllerBase, IStartable
    {
        private readonly PrepareSetting prepareSetting;
        private readonly CharacterSelectView characterSelectView;

        [Inject]
        public CharacterSelectPresenter(PrepareSetting prepareSetting, CharacterSelectView characterSelectView)
        {
            this.prepareSetting = prepareSetting;
            this.characterSelectView = characterSelectView;
        }

        public void Start()
        {
            characterSelectView.SelectedPlayerCharacterTypeObservable
                .Subscribe(characterType =>
                {
                    prepareSetting.SetSelectedPlayerCharacterType(characterType);
                })
                .AddTo(this);
        }
    }
}


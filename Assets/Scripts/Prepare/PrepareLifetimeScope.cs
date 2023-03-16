using Prepare;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PrepareLifetimeScope : LifetimeScope
{
    [SerializeField] private CharacterSelectView characterSelectView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<CharacterSelectPresenter>();

        builder.RegisterComponent(characterSelectView);
    }
}

using UnityEngine;

using UniRx;

using Runtime.Enemy.Component;

using InGame.DropItems;
using VContainer;
using VContainer.Unity;

namespace Runtime.Enemy.Util
{
    public class EnhancementPointObjectBinder : ControllerBase, IStartable
    {
        //各種マネージャー
        private readonly EnemyManager enemyManager;
        private readonly EnhancementPointObjectManager epoManager;

        [Inject]
        public EnhancementPointObjectBinder(EnemyManager enemyManager, EnhancementPointObjectManager enhancementPointObjectManager)
        {
            this.enemyManager = enemyManager;
            this.epoManager = enhancementPointObjectManager;
        }

        public void Start()
        {
            enemyManager.onGenerateEventHandler.Subscribe(enemy =>
            {
                EnemyItemDropper dropper = enemy.GetComponent<EnemyItemDropper>();
                dropper.initialize(epoManager.GenerateEnhancementPointObject);
            });
        }

    }
}

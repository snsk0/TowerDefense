using UnityEngine;


namespace Runtime.Enemy.Debug
{
    public class EnemyInitializer : MonoBehaviour
    {
        [SerializeField] private EnemyController controller;
        [SerializeField] private GameObject player;


        private void Start()
        {
            controller.Initialize(0);
            controller.Damage(0, 0, 100, player);
        }
    }
}

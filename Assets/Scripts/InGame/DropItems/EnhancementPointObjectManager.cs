using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using InGame.Players;
using System;

namespace InGame.DropItems
{
    public class EnhancementPointObjectManager : ControllerBase
    {
        private GameObject prefab;
        private Transform poolParent;
        private List<GameObject> objectPool = new List<GameObject>();
        private EnhancementPointObjectGenerator generator;
        private PlayerManager playerManager;
        private PlayerBackpack playerBackpack;

        public EnhancementPointObjectManager(GameObject prefab, Transform poolParent, EnhancementPointObjectGenerator generator,
            PlayerManager playerManager, PlayerBackpack playerBackpack)
        {
            this.prefab = prefab;
            this.poolParent = poolParent;
            this.generator = generator;
            this.playerManager = playerManager;
            this.playerBackpack = playerBackpack;
        }

        public void GenerateEnhancementPointObject(Vector3 generatePosition, int point)
        {
            var obj = objectPool.FirstOrDefault(x => !x.activeSelf);
            if (obj == null)
            {
                obj=generator.GenerateEnhancementPointObject(prefab);
                objectPool.Add(obj);
            }
            else
            {
                obj.SetActive(true);
            }

            InitPointObject(obj, generatePosition, point);
        }

        //生成したオブジェクトの初期化
        private void InitPointObject(GameObject obj, Vector3 generatePosition, int point)
        {
            obj.transform.position = generatePosition;
            obj.transform.SetParent(poolParent);

            EnhancementPoint enhancementPoint = obj.GetComponent<EnhancementPoint>();
            enhancementPoint.Init(point);
            ObservePointObject(enhancementPoint);
            
        }

        //ポイントが拾われるまで監視する
        private void ObservePointObject(EnhancementPoint enhancementPoint)
        {
            enhancementPoint.PickedObservable
                .Subscribe(_ =>
                {
                    Action callbackAction = () =>
                    {
                        enhancementPoint.gameObject.SetActive(false);
                        playerBackpack.AddEnhancementPoint(enhancementPoint.point);
                    };
                    enhancementPoint.MoveToPlayer(playerManager.currentPlayerObject.transform, callbackAction);
                })
                .AddTo(this);
        }
    }
}


using System.Collections.Generic;

using UnityEngine;

using UniRx;


namespace Runtime.Enemy.Component
{
    public class EnemyHate : MonoBehaviour, ITargetProvider
    {
        //ヘイト値管理
        private Dictionary<GameObject, float> hateMap;


        //target(変更イベント含む)
        private ReactiveProperty<GameObject> _target;
        public IReadOnlyReactiveProperty<GameObject> target => _target;





        //初期化
        public void Initialize()
        {
            hateMap = new Dictionary<GameObject, float>();
            _target = new ReactiveProperty<GameObject>();
        }



        //ヘイト値追加
        public void AddHate(float hate, GameObject gameObject)
        {
            //新規追加
            if (!hateMap.ContainsKey(gameObject))
            {
                hateMap.Add(gameObject, hate);
            }
            //ヘイト値加算
            else
            {
                float currentHate = hateMap[gameObject];
                hateMap[gameObject] = currentHate + hate;
            }

            //target更新
            _target.Value = GetMaxHateObject();
        }


        //クリア
        public void ClearHate(GameObject gameObject)
        {
            if (hateMap.ContainsKey(gameObject)) hateMap.Remove(gameObject);

            //target更新
            _target.Value = GetMaxHateObject();
        }



        //最大ヘイト値のオブジェクト取得
        private GameObject GetMaxHateObject()
        {
            //比較用
            GameObject gameObject = null;
            float maxHate = 0;

            //全検索
            foreach (KeyValuePair<GameObject, float> hatePair in hateMap)
            {
                if (maxHate < hatePair.Value)
                {
                    maxHate = hatePair.Value;
                    gameObject = hatePair.Key;
                }
            }

            //結果を返す
            return gameObject;
        }



        //終了時に破棄
        private void OnDisable()
        {
            hateMap = null;
            _target.Dispose();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;


namespace Runtime.Enemy.Component
{
    public class EnemyHate : MonoBehaviour
    {
        //ヘイト値管理
        private Dictionary<GameObject, float> hateMap;



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
        }



        //最大ヘイト値のオブジェクト取得
        public GameObject GetMaxHateObject()
        {
            //比較用
            GameObject gameObject = null;
            float maxHate = 0;

            //全検索
            foreach(KeyValuePair<GameObject, float> hatePair in hateMap)
            {
                if(maxHate < hatePair.Value)
                {
                    maxHate = hatePair.Value;
                    gameObject = hatePair.Key;
                }
            }

            //結果を返す
            return gameObject;
        }



        //初期化する
        private void OnEnable()
        {
            hateMap = new Dictionary<GameObject, float>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.DropItems
{
    public class EnhancementPointObjectGenerator : MonoBehaviour
    {
        public GameObject GenerateEnhancementPointObject(GameObject prefab)
        {
            var obj = Instantiate(prefab);
            return obj;
        }
    }
}


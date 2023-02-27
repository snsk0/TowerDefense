using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Enemy.Parameter
{
    public class EnemyHate : MonoBehaviour, IHate
    {
        public void AddHate(double hate, GameObject cause)
        {
            throw new System.NotImplementedException();
        }

        public void GetHate(GameObject cause)
        {
            throw new System.NotImplementedException();
        }

        public void RestHate()
        {
            throw new System.NotImplementedException();
        }

        public GameObject target { get; private set; }



        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.Find("Player");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


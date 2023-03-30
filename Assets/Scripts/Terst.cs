using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terst : MonoBehaviour
{
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 1)
        {
            Debug.Log(Vector3.Slerp(Vector3.zero, Vector3.one, time));
            time += Time.deltaTime;
        }
    }
}

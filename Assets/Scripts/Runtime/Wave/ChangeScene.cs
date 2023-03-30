using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Runtime.Wave
{
    public class ChangeScene : MonoBehaviour
    {

        [SerializeField] private string sceneName;
        public void OnButton()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

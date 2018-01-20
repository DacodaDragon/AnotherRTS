using BoneBox.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnotherRTS.Management
{
	public class GameManager : DDOLSingleton<GameManager>
	{
        public void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                int scene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
            }
        }
	}
}
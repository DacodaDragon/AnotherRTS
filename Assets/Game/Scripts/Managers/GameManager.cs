using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnotherRTS.Util;

namespace AnotherRTS.Management
{
	public class GameManager : DDOLSingleton<GameManager>
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}
	}
}
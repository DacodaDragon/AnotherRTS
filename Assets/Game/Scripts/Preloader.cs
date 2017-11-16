using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnotherRTS
{
	/// <summary>
	/// This class runs the second scene in the build settings (index 1) at Start.
	/// </summary>
	public class Preloader : MonoBehaviour
	{
		private void Start()
		{
			SceneManager.LoadScene(1);
		}
	}
}
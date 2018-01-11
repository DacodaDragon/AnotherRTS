using UnityEngine;

namespace BoneBox.SceneManagement
{
	public class SceneReloader : MonoBehaviour
	{
		private void Update()
		{
			if (Application.isEditor && Input.GetKeyUp(KeyCode.R))
			{
				SceneManager.ReloadActive();
			}
		}
	}
}

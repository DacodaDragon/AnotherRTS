using UnityEngine;

namespace BoneBox.SceneManagement.Util
{
	public class GlobalClickToScene : MonoBehaviour
	{
		[SerializeField] private string m_TargetScene;

		private void Update()
		{
			if (Input.GetMouseButtonUp(0)) {
				SceneManager.LoadScene(m_TargetScene);
			}
		}
	}
}
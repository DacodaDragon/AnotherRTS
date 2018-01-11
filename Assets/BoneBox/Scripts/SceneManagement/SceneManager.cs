using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace BoneBox.SceneManagement
{
	public static class SceneManager
	{
		public static void LoadScene(string name)
		{
			USceneManager.LoadScene(name);
		}

		public static void LoadScene(int buildID)
		{
			USceneManager.LoadScene(buildID);
		}

		public static void ReloadActive()
		{
			USceneManager.LoadScene(USceneManager.GetActiveScene().buildIndex);
		}
	}
}
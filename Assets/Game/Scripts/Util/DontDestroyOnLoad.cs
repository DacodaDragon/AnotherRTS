using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnotherRTS.Util
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		private void Start()
		{
			DontDestroyOnLoad(this.gameObject);
		}
	}
}
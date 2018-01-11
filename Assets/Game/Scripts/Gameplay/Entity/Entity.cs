using UnityEngine;

namespace AnotherRTS.Gameplay.Entity
{
	public class Entity : MonoBehaviour
	{
		public enum EType
		{
			Unit = 0,
			Building
		}

		private EType m_Type;

		public EType Type { get { return m_Type; } }
	}
}
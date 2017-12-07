using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnotherRTS.Gameplay
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
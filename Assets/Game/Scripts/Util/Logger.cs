using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnotherRTS.Util
{
	public class Logger
	{
		public const string PURPLE = "purple";
		public const string GREEN = "green";
		public const string BLUE = "blue";

		private string m_Label;

		public string Label { get { return m_Label; } }

		public Logger(object owner, string labelColour)
		{
			string rawOwnerName = owner.GetType().ToString();
			m_Label = $"[<color={labelColour}>{rawOwnerName.Substring(rawOwnerName.LastIndexOf('.') + 1)}</color>]";
		}

		public Logger(string owner, string labelColour)
		{
			m_Label = $"[<color={labelColour}>{owner}</color>]";
		}

		public void Log(object message)
		{
			Debug.Log($"{m_Label} {message}");
		}

		public void LogWarning(object message)
		{
			Debug.LogWarning($"{m_Label} {message}");
		}

		public void LogError(object message)
		{
			Debug.LogError($"{m_Label} {message}");
		}
	}
}
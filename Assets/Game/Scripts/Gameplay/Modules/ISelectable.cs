using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnotherRTS.Gameplay.Modules
{
	public interface ISelectable
	{
		bool IsSelected { get; set; }
	}
}
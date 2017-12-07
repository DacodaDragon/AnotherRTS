using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCamera = UnityEngine.Camera;
using AnotherRTS.Gameplay.Modules;

namespace AnotherRTS.Gameplay
{
	public class Selector : MonoBehaviour
	{
		private UCamera m_Camera;

		[SerializeField] private LayerMask m_SelectionLayers;

		private void Start()
		{
			m_Camera = UCamera.main;
		}

		private void Update()
		{
			if (Input.GetMouseButtonUp(0)) {
				Vector2 mousePosition = Input.mousePosition;
				RaycastHit hitInfo;
				bool hitSuccess = Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out hitInfo, m_Camera.farClipPlane, m_SelectionLayers);

				if (hitSuccess) {
					ISelectable selectable = hitInfo.collider.GetComponent<ISelectable>();
					if (selectable != null) {
						selectable.IsSelected = !selectable.IsSelected;
					}
				}
			}
		}
	}
}
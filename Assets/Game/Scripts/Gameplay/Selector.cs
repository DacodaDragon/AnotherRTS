using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCamera = UnityEngine.Camera;

namespace AnotherRTS.Gameplay
{
	public class Selector : MonoBehaviour
	{
		private UCamera m_Camera;

		[SerializeField] private LayerMask m_SelectionLayers;
		[SerializeField] private List<Entity> m_SelectedEntities;

		public List<Entity> SelectedEntities { get { return m_SelectedEntities; } }

		private void Awake()
		{
			m_SelectedEntities = new List<Entity>();
		}

		private void Start()
		{
			m_Camera = UCamera.main;
		}

		private void Update()
		{
			if (Input.GetMouseButtonUp(0)) {
				if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
					m_SelectedEntities.Clear();
				}

				Vector2 mousePosition = Input.mousePosition;
				RaycastHit hitInfo;
				bool hitSuccess = Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out hitInfo, m_Camera.farClipPlane, m_SelectionLayers);

				if (hitSuccess) {
					Entity entity = hitInfo.collider.GetComponent<Entity>();
					if (entity != null) {
						if (m_SelectedEntities.Contains(entity)) {
							m_SelectedEntities.Add(entity);
						}
					}
				}
			}
		}
	}
}
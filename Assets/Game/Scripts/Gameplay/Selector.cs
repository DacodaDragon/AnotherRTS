using System.Collections.Generic;

using UnityEngine;
using UCamera = UnityEngine.Camera;

using BoneBox.Core;
using Logger = BoneBox.Debug.Logger;

using AnotherRTS.Management.RemappableInput;
using AnotherRTS.Gameplay.Entities;

namespace AnotherRTS.Gameplay
{
	public class Selector : Singleton<Selector>
	{
		private UCamera m_Camera;
		private InputManager m_InputManager;
		private int m_SingleSelectKey;
		private int m_MultiSelectKey;
		private List<ISelectable> m_SelectedEntities;

		[SerializeField] private LayerMask m_SelectionLayers;

		public List<ISelectable> SelectedEntities { get { return m_SelectedEntities; } }

		private new void Awake()
		{
			base.Awake();
			m_SelectedEntities = new List<ISelectable>();
		}

		private void Start()
		{
			m_Camera = UCamera.main;
			m_InputManager = InputManager.Instance;
			m_SingleSelectKey = m_InputManager.GetKeyID("single select");
			m_MultiSelectKey = m_InputManager.GetKeyID("multi select");
		}

		private void TrySelect()
		{
			Vector2 mousePosition = Input.mousePosition;
			RaycastHit hitInfo;
			bool hitSuccess = Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out hitInfo, m_Camera.farClipPlane, m_SelectionLayers);

			if (hitSuccess)
			{
				ISelectable selectable = hitInfo.collider.GetComponent<ISelectable>();
				if (selectable != null)
				{
					if (!m_SelectedEntities.Contains(selectable))
					{
						m_SelectedEntities.Add(selectable);
						Logger.Log(this, $"Selected {selectable.ToString()}");
					}
				}
			}
		}

		public void DeselectAll()
		{
			m_SelectedEntities.Clear();
			Logger.Log(this, "Deselected all");
		}

		private void Update()
		{
			if (m_InputManager.GetKeyUp(m_SingleSelectKey))
			{
				DeselectAll();
				TrySelect();
			}

			if (m_InputManager.GetKeyUp(m_MultiSelectKey))
			{
				TrySelect();
			}
		}
	}
}
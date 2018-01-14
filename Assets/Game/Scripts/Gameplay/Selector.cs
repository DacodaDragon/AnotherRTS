using System.Collections.Generic;

using UnityEngine;
using UCamera = UnityEngine.Camera;

using BoneBox.Core;
using Logger = BoneBox.Debug.Logger;

using AnotherRTS.Management.RemappableInput;
using AnotherRTS.Gameplay.Entities;
using System;

namespace AnotherRTS.Gameplay
{
    public partial class Selector : Singleton<Selector>
    {
        private UCamera m_Camera;
        private InputManager m_InputManager;
        private int m_SingleSelectKey;
        private int m_MultiSelectKey;

        // TODO: Move commankey to commandable
        private int m_CommandKey;

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
            m_CommandKey = m_InputManager.GetKeyID("unit move");
            m_SelectionLayers.value = LayerMask.GetMask("Unit");
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
                        selectable.OnEntitySelect();
                        Logger.Log(this, $"Selected {selectable.ToString()}");
                    }
                }
            }
        }

        public void DeselectAll()
        {
            for (int i = 0; i < m_SelectedEntities.Count; i++)
            {
                m_SelectedEntities[i].OnEntityDeselect();
            }
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

            // TODO: Move commankey to commandable
            if (m_InputManager.GetKeyUp(m_CommandKey))
            {
                Vector2 mousePosition = Input.mousePosition;
                RaycastHit hitInfo;
                bool hitSuccess = Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out hitInfo, m_Camera.farClipPlane, LayerMask.GetMask("Default"));

                if (hitSuccess)
                {
                    for (int i = 0; i < m_SelectedEntities.Count; i++)
                    {
                        if (m_SelectedEntities[i] is ICommandableEntity<Unit>)
                        {
                            ((ICommandableEntity<Unit>)m_SelectedEntities[i])
                                .TaskManager.TaskAdd(new MoveTask(hitInfo.point));
                        }
                    }
                }
            }
        }
    }
}
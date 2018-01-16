using System.Collections.Generic;

using UnityEngine;
using UCamera = UnityEngine.Camera;

using BoneBox.Core;
using Logger = BoneBox.Debug.Logger;

using AnotherRTS.Management.RemappableInput;
using AnotherRTS.Gameplay.Entities;
using AnotherRTS.UI;

using AnotherRTS.Gameplay.Entities.Units;

using System;
namespace AnotherRTS.Gameplay
{
    public partial class Selector : Singleton<Selector>
    {
        [SerializeField] SelectionGraphic selection;
        public Vector2 m_startSelection;
        private UCamera m_Camera;
        private InputManager m_InputManager;
        private int m_SingleSelectKey;
        private int m_MultiSelectKey;
        private int m_DragAddSelectionKey;
        private int m_DragSelectKey;
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
            selection.OnSelectionRelease += RecieveSelectionRect;
        }

        private void TrySelect()
        {
            Vector2 mousePosition = Input.mousePosition;
            RaycastHit hitInfo;
            bool hitSuccess = Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out hitInfo, m_Camera.farClipPlane, m_SelectionLayers);

            if (hitSuccess)
            {
                ISelectable selectable = hitInfo.collider.GetComponent<ISelectable>();

                if (selectable == null)
                {
                    selectable = hitInfo.collider.GetComponentInParent<ISelectable>();
                }

                if (selectable == null)
                    return;

                if (!m_SelectedEntities.Contains(selectable))
                {
                    m_SelectedEntities.Add(selectable);
                    selectable.OnEntitySelect();
                    Logger.Log(this, $"Selected {selectable.ToString()}");
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

            if (m_InputManager.GetKeyDown(m_SingleSelectKey))
            {
                m_startSelection = Input.mousePosition;
            }

            if (m_InputManager.GetKey(m_SingleSelectKey))
            {
                if (Vector2.Distance(m_startSelection, Input.mousePosition) > 4)
                {
                    selection.Enable(m_startSelection);
                    gameObject.SetActive(false);
                }
            }

            if (m_InputManager.GetKeyUp(m_CommandKey))
            {
                Vector2 mousePosition = Input.mousePosition;
                RaycastHit hitInfo;
                bool hitSuccess = Physics.Raycast(m_Camera.ScreenPointToRay(mousePosition), out hitInfo, m_Camera.farClipPlane);

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

        private void RecieveSelectionRect(Rect r)
        {
            DeselectAll();

            UnitManager manager = UnitManager.Instance;
            EntityScreenInfo<Unit>[] info = manager.GetAllUnitsInScreen();

            for (int i = 0; i < info.Length; i++)
            {
                if (r.Contains(info[i].position))
                {
                    m_SelectedEntities.Add(info[i].context);
                    info[i].context.OnEntitySelect();
                }
            }

            gameObject.SetActive(true);
        }
    }
}
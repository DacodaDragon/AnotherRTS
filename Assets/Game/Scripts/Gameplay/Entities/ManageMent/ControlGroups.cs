using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnotherRTS.Gameplay.Entities;
using AnotherRTS.Management.RemappableInput;
using AnotherRTS.Gameplay.Entities.Units;

using Logger = BoneBox.Debug.Logger;

namespace AnotherRTS.Gameplay
{
    public class ControlGroups : MonoBehaviour
    {
        InputManager inputmanager;
        Selector selector;
        UnitManager unitManager;

        List<ISelectable>[] Entities = new List<ISelectable>[10];
        private int[] m_KeySelectID = new int[10];
        private int[] m_KeyAssignID = new int[10];
        private float[] m_KeySelectDoubleTap = new float[10];
        private float m_doubleTapDelay = 0.25f;

        public void Start()
        {
            inputmanager = InputManager.Instance;
            selector = Selector.Instance;
            unitManager = UnitManager.Instance;
            unitManager.OnUnitLost += RemoveEntity;

            for (int i = 0; i < Entities.Length; i++)
            {
                Entities[i] = new List<ISelectable>(30);
            }

            for (int i = 0; i < 9; i++)
            {
                m_KeySelectID[i] = inputmanager.GetKeyID(string.Format("unit select group {0}", i));
                m_KeyAssignID[i] = inputmanager.GetKeyID(string.Format("unit assign group {0}", i));
            }
        }

        public void RemoveEntity(ISelectable entity)
        {
            for (int i = 0; i < Entities.Length; i++)
            {
                if (Entities[i] == null)
                    continue;

                if (Entities[i].Contains(entity))
                {
                    Entities[i].Remove(entity);
                }
            }
        }

        public void Update()
        {
            for (int i = 0; i < 9; i++)
            {
                if (inputmanager.GetKeyDown(m_KeyAssignID[i]))
                {
                    Entities[i].Clear();
                    Entities[i].AddRange(selector.SelectedEntities);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (inputmanager.GetKeyDown(m_KeySelectID[i]))
                {
                    if (Entities[i].Count == 0)
                        break;

                    if (Time.unscaledTime < m_KeySelectDoubleTap[i] + m_doubleTapDelay)
                    {
                        Camera.CameraFocusMove.Instance.Goto(GroupToPosition(Entities[i].ToArray()));
                    }

                    m_KeySelectDoubleTap[i] = Time.unscaledTime;
                    selector.SetSelection(Entities[i].ToArray());
                }
            }
        }

        private Vector3 GroupToPosition(ISelectable[] selectables)
        {
            Vector3 position = Vector3.zero;
            for (int i = 0; i < selectables.Length; i++)
            {
                if (selectables[i] is Entity)
                {
                    position += (((Entity)selectables[i]).transform.position);
                }
            }
            return position / selectables.Length;
        }
    }
}

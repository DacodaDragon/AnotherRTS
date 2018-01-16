using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnotherRTS.Gameplay.Entities;
using AnotherRTS.Management.RemappableInput;

using Logger = BoneBox.Debug.Logger;

namespace AnotherRTS.Gameplay
{
    public class ControlGroups : MonoBehaviour
    {
        InputManager inputmanager;
        Selector selector;

        ISelectable[][] Entities = new ISelectable[10][];
        private int[] m_KeySelectID = new int[10];
        private int[] m_KeyAssignID = new int[10];
        private float[] m_KeySelectDoubleTap = new float[10];
        private float m_doubleTapDelay = 0.25f;

        public void Start()
        {
            inputmanager = InputManager.Instance;
            selector = Selector.Instance;
            for (int i = 0; i < 9; i++)
            {
                m_KeySelectID[i] = inputmanager.GetKeyID(string.Format("unit select group {0}", i));
                m_KeyAssignID[i] = inputmanager.GetKeyID(string.Format("unit assign group {0}", i));
            }
        }

        public void Update()
        {
            for (int i = 0; i < 9; i++)
            {
                if (inputmanager.GetKeyDown(m_KeyAssignID[i]))
                {
                    Entities[i] = selector.SelectedEntities.ToArray();
                    Logger.Log(this, "Grouped " + Entities[i].Length + " entities on group " + i);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (inputmanager.GetKeyDown(m_KeySelectID[i]))
                {
                    if (Entities[i] == null)
                        break;

                    //if (Time.unscaledTime < m_KeySelectDoubleTap[i] + m_doubleTapDelay)
                    //{ break; } // TODO fire some event

                    m_KeySelectDoubleTap[i] = Time.unscaledTime;
                    selector.SetSelection(Entities[i]);
                }
            }
        }
    }
}

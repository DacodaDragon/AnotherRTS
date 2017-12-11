using System.Collections.Generic;
using UnityEngine;

namespace AnotherRTS.Management.RemappableInput
{
    public class KeyBindingDatabase
    {
        Dictionary<string, int> m_NameIDPairs;
        Dictionary<int, ControlGroup> m_GroupDict;
        ControlGroup[] m_Groups;

        public KeyBindingDatabase(ControlGroup[] groups, Dictionary<string,int> nameId)
        {
            m_Groups = groups;
            m_NameIDPairs = nameId;
            SetControlGroups(groups);
        }

        private void SetControlGroups(ControlGroup[] groups)
        {
            m_GroupDict = new Dictionary<int, ControlGroup>();
            // Link all key ID's to their respective control groups
            // So we can find them back quickly later.
            for (int i = 0; i < groups.Length; i++)
            {
                int[] IDs = groups[i].GetAllKeyIDs();
                for (int j = 0; j < IDs.Length; j++)
                {
                    m_GroupDict.Add(IDs[j],groups[i]);
                }
            }
        }

        private ControlGroup FindContainingGroup(int id)
        {
            ControlGroup group;

            if (!m_GroupDict.TryGetValue(id, out group))
                throw new System.Exception("ControlGroup with id: " + id + "not found.");

            return group;
        }

        public void KeyUp(KeyCode keycode)
        {
            for (int i = 0; i < m_Groups.Length; i++)
            {
                m_Groups[i].KeyUp(keycode);
            }
        }

        public void KeyDown(KeyCode keycode)
        {
            for (int i = 0; i < m_Groups.Length; i++)
            {
                m_Groups[i].KeyDown(keycode);
            }
        }

        public bool GetKeyUp(int id)
        {
            return FindContainingGroup(id).GetKey(id).IsReleased;
        }

        public bool GetKey(int id)
        {
            return FindContainingGroup(id).GetKey(id).IsHeld;
        }

        public bool GetKeyDown(int id)
        {
            return FindContainingGroup(id).GetKey(id).IsPressed;
        }

        public int GetKeyID(string name)
        {
            int id;
            if (!m_NameIDPairs.TryGetValue(name, out id))
                throw new System.Exception("Keybinding \"" + name + "\" doesn't exist!");
            return id;
        }
    }
}

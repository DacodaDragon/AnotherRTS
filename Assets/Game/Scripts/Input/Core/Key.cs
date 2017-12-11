using UnityEngine;

namespace AnotherRTS.Management.RemappableInput
{
    public class Key
    {
        private int m_id;
        private string m_name;
        private KeyCode[] m_keys;
        private KeyCode[] m_modifiers;
        private int m_framePress = 0;
        private int m_frameRelease = 0;
        private bool m_held = false;

        public int       ID        { get { return m_id;   } }
        public string    Name      { get { return m_name; } }
        public KeyCode[] Keys      { get { return m_keys; }      set { m_keys = value; } }
        public KeyCode[] Modifiers { get { return m_modifiers; } set { m_keys = value; } }
        public bool IsPressed      { get { return (m_framePress == Time.frameCount); } }
        public bool IsReleased     { get { return (m_frameRelease == Time.frameCount); } }
        public bool IsHeld         { get { return m_held; } }

        public Key(int id, string name, KeyCode[] keys, KeyCode[] modifiers)
        {
            m_id = id;
            m_name = name;
            m_keys = keys;
            m_modifiers = modifiers;
        }

        // [TODO] Fix Input to update when keys are released. 
        public void OnKeyDown(KeyCode KeyCode)
        {
            if (m_held)
                return;

            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i] == KeyCode)
                {
                    m_framePress = Time.frameCount;
                    m_held = true;
                    return;
                }
            }
        }

        public void OnKeyUp(KeyCode KeyCode)
        {
            if (!m_held)
                return;

            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i] == KeyCode)
                {
                    m_frameRelease = Time.frameCount;
                    m_held = false;
                    return;
                }
            }
        }
    }
}

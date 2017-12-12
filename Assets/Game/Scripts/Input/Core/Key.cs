using UnityEngine;
using AnotherRTS.Util;

namespace AnotherRTS.Management.RemappableInput
{
    public class Key
    {
        private int m_id;
        private string m_name;
        private KeyCode[] m_keys;
        private KeyCode[] m_modifiers;
        private bool[] m_keysPressed;
        private bool[] m_modifiersPressed;
        private int m_framePress = 0;
        private int m_frameRelease = 0;
        private bool m_held = false;

        public int       ID         { get { return m_id;   } }
        public string    Name       { get { return m_name; } }
        public KeyCode[] Keys       { get { return m_keys; }      set { m_keys = value; } }
        public KeyCode[] Modifiers  { get { return m_modifiers; } set { m_keys = value; } }
        public bool      IsPressed  { get { return (m_framePress == Time.frameCount); } }
        public bool      IsReleased { get { return (m_frameRelease == Time.frameCount); } }
        public bool      IsHeld     { get { return m_held; } }

        public Key(int id, string name, KeyCode[] keys, KeyCode[] modifiers)
        {
            m_id = id;
            m_name = name;
            m_keys = keys;
            m_modifiers = modifiers;

            m_keysPressed = new bool[keys.Length];
            m_modifiersPressed = new bool[modifiers.Length];

            // Init all keys to not be pressed
            ArrayUtil.Fill(m_keysPressed, false);
            ArrayUtil.Fill(m_modifiersPressed, false);
        }

        // [TODO] Fix Input to update when keys are released. 
        public void OnKeyDown(KeyCode KeyCode)
        {
            for (int i = 0; i < m_modifiers.Length; i++)
            {
                if (m_modifiers[i] == KeyCode)
                {
                    m_modifiersPressed[i] = true;
                    m_held = (ArrayUtil.Contains(m_keysPressed, true)
                             && ArrayUtil.AllEqual(m_modifiersPressed, true));
                    return;
                }
            }

            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i] == KeyCode)
                {
                    m_keysPressed[i] = true;

                    if (!m_held)
                        m_framePress = Time.frameCount;

                    if (ArrayUtil.AllEqual(m_modifiersPressed,true))
                        m_held = true;

                    return;
                }
            }
        }

        public void OnKeyUp(KeyCode KeyCode)
        {
            for (int i = 0; i < m_modifiers.Length; i++)
            {
                if (m_modifiers[i] == KeyCode)
                {
                    // If one of the modifiers dissapear
                    // we CAN'T be holding the correct
                    // key combination
                    m_modifiersPressed[i] = false;
                    m_held = false;
                    return;
                }
            }

            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i] == KeyCode)
                {
                    m_keysPressed[i] = false;
                    if (!ArrayUtil.Contains(m_keysPressed,true) || !ArrayUtil.AllEqual(m_modifiersPressed,true))
                    {
                        if (m_held)
                            m_frameRelease = Time.frameCount;
                        m_held = false;
                    }
                    return;
                }
            }
        }

        public void Panic()
        {
            ArrayUtil.Fill(m_keysPressed, false);
            ArrayUtil.Fill(m_modifiersPressed, false);
            m_held = false;
        }
    }
}

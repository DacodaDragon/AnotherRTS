using System.Collections.Generic;
using AnotherRTS.Management.RemappableInput.IO;

namespace AnotherRTS.Management.RemappableInput
{
    public class KeybindingDatabaseFactory
    {
        private int m_IdIndex = 0;
        private Dictionary<string, int> m_nameID = new Dictionary<string, int>();

        public KeyBindingDatabase Build(ControlGroupData[] data)
        {
            m_nameID.Clear();
            m_IdIndex = 0;

            ControlGroup[] ControlGroups = new ControlGroup[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                ControlGroups[i] = CreateControlGroup(data[i]);
            }
            return new KeyBindingDatabase(ControlGroups, m_nameID);
        }

        private ControlGroup CreateControlGroup(ControlGroupData group)
        {
            Key[] keys = new Key[group.keys.Length];
            for (int i = 0; i < group.keys.Length; i++)
            {
                keys[i] = CreateKey(group.keys[i]);
            }
            return new ControlGroup(group.name,keys);
        }

        private Key CreateKey(KeyData key)
        {
            m_nameID.Add(key.name, m_IdIndex);
            return new Key(m_IdIndex++,key.name, key.keycodes, key.modifiers);
        }
    }
}

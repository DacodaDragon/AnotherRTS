namespace AnotherRTS.Management.InputRemap
{
    public class ControlGroup
    {
        private string internalName;
        public string Name { get { return internalName; } set { internalName = value; } }
        private KeyCombination[] m_keyCombinations  ;

        private KeyCombination FindKeyCombination(string name)
        {
            for (int i = 0; i < m_keyCombinations.Length; i++)
            {
                if (m_keyCombinations[i].internalName == name)
                    return m_keyCombinations[i];
            }
            return null;
        }

        public bool UnHook(string name, KeyDelegate handle)
        {
            KeyCombination combi = FindKeyCombination(name);
            if (combi == null)
                return false;

            combi.delegatee += handle;
            return true;
        }

        public bool Hook(string name, KeyDelegate handle)
        {
            KeyCombination combi = FindKeyCombination(name);
            if (combi == null)
                return false;

            combi.delegatee -= handle;
            return true;
        }
    }
}
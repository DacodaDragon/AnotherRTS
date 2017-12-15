using UnityEngine;

using AnotherRTS.Management.RemappableInput.IO;
namespace AnotherRTS.Management.RemappableInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        TextAsset asset;
        KeyBindingDatabase m_database;

        public void Awake()
        {
            YamlControlSchemeReader reader = new YamlControlSchemeReader();
            KeybindingDatabaseFactory factory = new KeybindingDatabaseFactory();
            m_database = factory.Build(reader.FromString(asset.text));

            // Second init phase
            m_database.Start();
        }

        private void OnGUI()
        {
            if (Event.current.type == EventType.keyDown)
            {
                m_database.KeyDown(Event.current.keyCode);
            }

            if (Event.current.type == EventType.keyUp)
            {
                m_database.KeyUp(Event.current.keyCode);
            }
        }

        public bool GetKeyUp(int id)
        {
            return m_database.GetKeyUp(id);
        }

        public bool GetKeyDown(int id)
        {
            return m_database.GetKeyDown(id);
        }

        public bool GetKey(int id)
        {
            return m_database.GetKey(id);
        }

        public int GetKeyID(string name)
        {
            return m_database.GetKeyID(name);
        }
    }
}

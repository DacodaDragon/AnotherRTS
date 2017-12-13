using UnityEngine;

using AnotherRTS.Management.RemappableInput.IO;

namespace AnotherRTS.Management.RemappableInput
{
    public class InputManager : DDOLSingleton<InputManager>
    {
        [SerializeField]
        TextAsset asset;
        KeyBindingDatabase m_database;


        public string[] KeyNames { get { return m_database.KeyNames; } }

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
                m_database.KeyDown(Event.current.keyCode);

            if (Event.current.type == EventType.keyUp)
                m_database.KeyUp(Event.current.keyCode);

            if (Event.current.type == EventType.mouseDown)
                m_database.KeyDown(MouseToKey(Event.current.button));

            if (Event.current.type == EventType.mouseUp)
                m_database.KeyUp(MouseToKey(Event.current.button));
        }

        private KeyCode MouseToKey(int num)
        {
            switch (num)
            {
                case 0: return KeyCode.Mouse0;
                case 1: return KeyCode.Mouse1;
                case 2: return KeyCode.Mouse2;
                case 3: return KeyCode.Mouse3;
                case 4: return KeyCode.Mouse4;
                case 5: return KeyCode.Mouse5;
                case 6: return KeyCode.Mouse6;
                default: return KeyCode.None;
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

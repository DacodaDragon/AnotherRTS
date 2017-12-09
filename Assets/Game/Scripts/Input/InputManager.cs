using System.IO;
using System.Collections.Generic;

using YamlDotNet.RepresentationModel;
using UnityEngine;


namespace AnotherRTS.Management.RemappableInput.IO
{
    public class YamlControlSchemeReader
    {
        public void FromString(string content)
        {
            StringReader reader = new StringReader(content);
            YamlStream yaml = new YamlStream();
            yaml.Load(reader);

            List<ControlGroup> controlgroups = new List<ControlGroup>();
            for (int i = 0; i < yaml.Documents.Count; i++)
            {
               controlgroups.Add(HandleControlGroup((YamlMappingNode)yaml.Documents[i].RootNode));
            }

            for (int i = 0; i < controlgroups.Count; i++)
            {
                Debug.Log(controlgroups[i].ToString());
            }
        }

        public ControlGroup HandleControlGroup(YamlMappingNode node)
        {
            string name = (string)node.Children[new YamlScalarNode("name")];
            Key[] keys = ReadKeys((YamlSequenceNode)node.Children[new YamlScalarNode("keys")]);
            return new ControlGroup(name, keys);
        }

        public Key[] ReadKeys(YamlSequenceNode node)
        {
            List<Key> keys = new List<Key>();
            for (int i = 0; i < node.Children.Count; i++)
            {
                string name = (string)node[i][new YamlScalarNode("name")];
                KeyCode[] keycodes = HandleKeyCodes((YamlSequenceNode)node[i][new YamlScalarNode("keycodes")]);
                KeyCode[] modifiers = HandleKeyCodes((YamlSequenceNode)node[i][new YamlScalarNode("modifiers")]);
                keys.Add(new Key(name, keycodes, modifiers));
            }
            return keys.ToArray();
        }

        private KeyCode[] HandleKeyCodes(YamlSequenceNode node)
        {
            string[] codenames = HandleSequence(node);
            KeyCode[] KeyCodes = new KeyCode[codenames.Length];
            for (int i = 0; i < codenames.Length; i++)
            {
                KeyCodes[i] = (KeyCode)System.Enum.Parse(
                    typeof(KeyCode), codenames[i]);
            }
            return KeyCodes;
        }

        private string[] HandleSequence(YamlSequenceNode node)
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < node.Children.Count; i++)
            {
                strings.Add((string)node[i]);
            }
            return strings.ToArray();
        }
    }

    public struct ControlGroup
    {
        public readonly string name;
        public readonly Key[] keys;

        public ControlGroup(string name, Key[] keys)
        {
            this.name = name;
            this.keys = keys;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(name);
            sb.Append('\n');
            for (int i = 0; i < keys.Length; i++)
            {
                sb.Append(keys[i].ToString());
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }

    public struct Key
    {
        public readonly string name;
        public readonly KeyCode[] keycodes;
        public readonly KeyCode[] modifiers;

        public Key(string name, KeyCode[] keycodes, KeyCode[] modifiers)
        {
            this.name = name;
            this.keycodes = keycodes;
            this.modifiers = modifiers;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Key: " + name);
            sb.Append("\n\tKeycode :");

            if (keycodes != null)
            {
                for (int i = 0; i < keycodes.Length; i++)
                {
                    sb.Append(" " + keycodes[i]);
                }
            }

            sb.Append("\n\tModifiers :");
            if (modifiers != null)
            {
                for (int i = 0; i < modifiers.Length; i++)
                {
                    sb.Append(" " + modifiers[i]);
                }
            }

            return sb.ToString();
        }
    }
}

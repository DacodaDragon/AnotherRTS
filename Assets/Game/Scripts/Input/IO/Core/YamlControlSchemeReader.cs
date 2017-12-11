﻿using System.IO;
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

            List<ControlGroupData> controlgroups = new List<ControlGroupData>();
            for (int i = 0; i < yaml.Documents.Count; i++)
            {
               controlgroups.Add(HandleControlGroup((YamlMappingNode)yaml.Documents[i].RootNode));
            }

            for (int i = 0; i < controlgroups.Count; i++)
            {
                Debug.Log(controlgroups[i].ToString());
            }
        }

        public ControlGroupData HandleControlGroup(YamlMappingNode node)
        {
            string name = (string)node.Children[new YamlScalarNode("name")];
            KeyData[] keys = ReadKeys((YamlSequenceNode)node.Children[new YamlScalarNode("keys")]);
            return new ControlGroupData(name, keys);
        }

        public KeyData[] ReadKeys(YamlSequenceNode node)
        {
            List<KeyData> keys = new List<KeyData>();
            for (int i = 0; i < node.Children.Count; i++)
            {
                string name = (string)node[i][new YamlScalarNode("name")];
                KeyCode[] keycodes = HandleKeyCodes((YamlSequenceNode)node[i][new YamlScalarNode("keycodes")]);
                KeyCode[] modifiers = HandleKeyCodes((YamlSequenceNode)node[i][new YamlScalarNode("modifiers")]);
                keys.Add(new KeyData(name, keycodes, modifiers));
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
}

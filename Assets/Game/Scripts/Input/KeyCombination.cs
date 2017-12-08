using UnityEngine;

namespace AnotherRTS.Management.InputRemap
{
    // TODO Fix this..
    public class KeyCombination
    {
        public string internalName;
        public KeyCode[] Modifier;
        public KeyCode[] keyCode;
        public KeyDelegate delegatee;
    }
}
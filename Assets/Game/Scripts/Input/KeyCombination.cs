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

        public void CheckKey(KeyCode key)
        {
            for (int i = 0; i < Modifier.Length; i++)
            {
                if (!Input.GetKey(Modifier[i]))
                    return;
            }

            for (int i = 0; i < keyCode.Length; i++)
            {
                if (keyCode[i] == key)
                {
                    delegatee.Invoke();
                    return;
                }
            }
        }
    }
}
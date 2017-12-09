using UnityEngine;

namespace AnotherRTS.Management.InputRemap
{
    public delegate void KeyDelegate();

    public class InputSystem : MonoBehaviour
    {
        ControlScheme Scheme = new ControlScheme();

        public void Update()
        {

        }
    }
}
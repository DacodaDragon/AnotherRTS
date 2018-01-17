using UnityEngine;
using AnotherRTS.Management.RemappableInput;
namespace AnotherRTS.Camera
{
    public class CameraControlKeyboard : ICameraMovementBehaviour
    {
        InputManager m_input;

        int[] Keys = new int[8];

        public CameraControlKeyboard()
        {
            m_input = InputManager.Instance;
            Keys[0] = m_input.GetKeyID("camera up");
            Keys[1] = m_input.GetKeyID("camera down");
            Keys[2] = m_input.GetKeyID("camera left");
            Keys[3] = m_input.GetKeyID("camera right");
            Keys[4] = m_input.GetKeyID("camera zoom in");
            Keys[5] = m_input.GetKeyID("camera zoom out");
            Keys[6] = m_input.GetKeyID("camera rotate left");
            Keys[7] = m_input.GetKeyID("camera rotate right");
        }

        public Vector4 Move()
        {
            float x, y, z, w; x = y = z = w = 0;

            if (m_input.GetKey(Keys[0]))
            {
                y += 1;
            }
            if (m_input.GetKey(Keys[1]))
            {
                y -= 1;
            }
            if (m_input.GetKey(Keys[2]))
            {
                x -= 1;
            }
            if (m_input.GetKey(Keys[3]))
            {
                x += 1;
            }
            if (m_input.GetKey(Keys[4]))
            {
                z += 1;
            }
            if (m_input.GetKey(Keys[5]))
            {
                z -= 1;
            }
            if (m_input.GetKey(Keys[6]))
            {
                w += 1;
            }
            if (m_input.GetKey(Keys[7]))
            {
                w -= 1;
            }

            //if (Input.GetKey(KeyCode.LeftControl))
            //{
            //    z = Input.GetAxisRaw("Vertical");
            //    w = Input.GetAxisRaw("Horizontal");
            //}
            //else
            //{
            //    x = Input.GetAxisRaw("Horizontal");
            //    y = Input.GetAxisRaw("Vertical");
            //}

            return new Vector4(x,y,z,w);
        }
    }
}

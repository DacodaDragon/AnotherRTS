using UnityEngine;

namespace AnotherRTS.Camera
{
    public class CameraControlKeyboard : ICameraMovementBehaviour
    {
        public Vector4 Move()
        {
            float x, y, z, w; x = y = z = w = 0;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                z = Input.GetAxisRaw("Vertical");
                w = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
            }

            return new Vector4(x,y,z,w);
        }
    }
}

using UnityEngine;

namespace AnotherRTS.Camera
{
    public class CameraControllerCameraBorders : ICameraMovementBehaviour
    {
        int m_PixelBorder = 5;
        public Vector4 Move()
        {
            float x, y, z, w;
            x = y = z = w = 0;

            Vector2 mpos = Input.mousePosition;

            if (mpos.y < m_PixelBorder)
                y = -1;
            if (mpos.x < m_PixelBorder)
                x = -1;

            if (mpos.y > Screen.height - m_PixelBorder)
                y = 1;
            if (mpos.x > Screen.width - m_PixelBorder)
                x = 1;

            return new Vector4(x, y, z, w);
        }
    }
}

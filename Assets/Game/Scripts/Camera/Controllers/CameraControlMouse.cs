using System;
using UnityEngine;

namespace AnotherRTS.Camera
{
    public class CameraControllsMouse : ICameraMovementBehaviour
    {
        private Vector2 m_MouseStartPos;
        private int m_PixelThreshhold = 10;
        private bool m_IsMoving;
        private float m_Sensitivity = 0;

        public CameraControllsMouse(float Sensitivity, int MoveMouseButton)
        {
            m_Sensitivity = Sensitivity;
        }

        public Vector4 Move()
        {
            float x, y, z, w;
            x = y = z = w = 0;

            // Mouse down (activate) 
            if (Input.GetMouseButtonDown(1))
            {
                m_MouseStartPos = Input.mousePosition;
            }

            // Mouse up  (deactivate) 
            if (Input.GetMouseButtonUp(1))
            {
                m_MouseStartPos = Vector2.zero;
                m_IsMoving = false;
            }

            // While mouse down (Check)
            if (Input.GetMouseButton(1))
            {
                if ((m_MouseStartPos-(Vector2)Input.mousePosition).magnitude > 10)
                {
                    m_IsMoving = true;
                }
            }

            // While moving (Update) 
            if (m_IsMoving)
            {
                // Move direction
                Vector2 moveVec = (m_MouseStartPos - (Vector2)Input.mousePosition);
                x = -moveVec.x * m_Sensitivity; // put x in Vec4
                y = -moveVec.y * m_Sensitivity; // put y in Vec4
            }

            return new Vector4(x, y, z, w);
        }
    }
}

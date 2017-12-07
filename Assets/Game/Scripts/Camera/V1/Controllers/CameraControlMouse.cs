using System;
using UnityEngine;

namespace AnotherRTS.Camera
{
    public class CameraControlsMouse : ICameraMovementBehaviour
    {
        private Vector2 m_moveStartPos;
        private Vector2 m_scrollStartPos;
        private int m_PixelThreshhold = 10;
        private bool m_IsMoving = false;
        private bool m_IsScrolling = false;
        private float m_Sensitivity = 0;

        public CameraControlsMouse(float Sensitivity, int MoveMouseButton)
        {
            m_Sensitivity = Sensitivity;
        }

        public Vector4 Move()
        {
            Vector4 moveVec = Vector4.zero;

            HandleMovement(ref moveVec);
            HandleRotation(ref moveVec);

            moveVec.z = Input.mouseScrollDelta.y;

            return moveVec;
        }


        public void HandleMovement(ref Vector4 moveVec)
        {
            // Mouse down (activate) 
            if (Input.GetMouseButtonDown(1))
            {
                m_moveStartPos = Input.mousePosition;
            }

            // Mouse up  (deactivate) 
            if (Input.GetMouseButtonUp(1))
            {
                m_moveStartPos = Vector2.zero;
                m_IsMoving = false;
            }

            // While mouse down (Check)
            if (Input.GetMouseButton(1))
            {
                if ((m_moveStartPos - (Vector2)Input.mousePosition).magnitude > 10)
                {
                    m_IsMoving = true;
                }
            }

            // While moving (Update) 
            if (m_IsMoving)
            {
                // Move direction
                Vector2 m_mouseDirection = (m_moveStartPos - (Vector2)Input.mousePosition);
                moveVec.x = -m_mouseDirection.x * m_Sensitivity; // put x in Vec4
                moveVec.y = -m_mouseDirection.y * m_Sensitivity; // put y in Vec4
            }
        }
        public void HandleRotation(ref Vector4 moveVec)
        {
            if (Input.GetMouseButtonDown(2))
            {
                m_scrollStartPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(2))
            {
                moveVec.w = ((m_scrollStartPos.x - Input.mousePosition.x) * m_Sensitivity);
            }
        }
    }
}

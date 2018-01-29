using UnityEngine;
using AnotherRTS.Management.RemappableInput;
using System;

namespace AnotherRTS.Camera
{
    public class CameraControlsMouse : ICameraMovementBehaviour
    {
        private InputManager m_input;
        private Vector2 m_moveStartPos;
        private Vector2 m_scrollStartPos;
        private int m_PixelThreshhold = 10;
        private bool m_IsMoving = false;
        private bool m_IsScrolling = false;
        private float m_MoveSensitivity = 0;
        private float m_ScrollSensitivity = 0;

        private int m_scrollKey;
        private int m_ZoomInKey;
        private int m_ZoomOutKey;

        public CameraControlsMouse(float Sensitivity, float MoveMouseButton)
        {
            m_input = InputManager.Instance;
            m_scrollKey = m_input.GetKeyID("global mouse right");
            m_ZoomInKey = m_input.GetKeyID("global scroll up");
            m_ZoomOutKey = m_input.GetKeyID("global scroll down");

            m_MoveSensitivity = Sensitivity;
        }

        public Vector4 Move()
        {
            Vector4 moveVec = Vector4.zero;

            HandleMovement(ref moveVec);
            HandleRotation(ref moveVec);
            HandleZoom(ref moveVec);
            moveVec.z = Input.mouseScrollDelta.y;

            return moveVec;
        }

        private void HandleZoom(ref Vector4 moveVec)
        {
            if (m_input.GetKey(m_ZoomInKey))
            {
                moveVec.y += m_ScrollSensitivity;
            }

            if (m_input.GetKey(m_ZoomOutKey))
            {
                moveVec.y -= m_ScrollSensitivity;
            }
        }

        public void HandleMovement(ref Vector4 moveVec)
        {
            // Mouse down (activate) 
            if (m_input.GetKeyDown(m_scrollKey))
            {
                m_moveStartPos = Input.mousePosition;
            }

            // Mouse up  (deactivate) 
            if (m_input.GetKeyUp(m_scrollKey) && m_IsMoving)
            {
                m_moveStartPos = Vector2.zero;
                m_IsMoving = false;
                Debug.Log("Enabled Unit Commands");

                m_input.SetActiveKeyGroup("units", true);


            }

            // While mouse down (Check)
            if (m_input.GetKey(m_scrollKey) && !m_IsMoving)
            {
                if ((m_moveStartPos - (Vector2)Input.mousePosition).magnitude > 10)
                {
                    m_IsMoving = true;
                    Debug.Log("Disabled Unit Commands");
                    m_input.SetActiveKeyGroup("units", false);
                }
            }

            // While moving (Update) 
            if (m_IsMoving)
            {
                // Move direction
                Vector2 m_mouseDirection = (m_moveStartPos - (Vector2)Input.mousePosition);
                moveVec.x = -m_mouseDirection.x * m_MoveSensitivity; // put x in Vec4
                moveVec.y = -m_mouseDirection.y * m_MoveSensitivity; // put y in Vec4
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
                moveVec.w = ((m_scrollStartPos.x - Input.mousePosition.x) * m_MoveSensitivity);
            }
        }
    }
}

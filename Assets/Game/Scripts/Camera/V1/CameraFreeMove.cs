 using System;
using UnityEngine;

namespace AnotherRTS.Camera
{
    public class CameraFreeMove : MonoBehaviour
    {
        [SerializeField] private float m_SpeedMultiplier;
        private Transform m_CamPosHelper;

        private CameraControlsManager m_CameraMovemenManager
            = new CameraControlsManager();

        private void Start()
        {
            m_CamPosHelper = new GameObject("CamPositionHelper").transform;
            m_CamPosHelper.SetParent(transform);
            m_CameraMovemenManager.Add(new CameraControlKeyboard());
            m_CameraMovemenManager.Add(new CameraControlsMouse(0.02f, 1));

            if (!Debug.isDebugBuild)
            m_CameraMovemenManager.Add(new CameraControllerCameraBorders());
        }

        private void UpdatePositionHelper(Vector3 pos, Vector3 rot)
        {
            m_CamPosHelper.position = pos;
            m_CamPosHelper.eulerAngles = new Vector3(0, rot.y, 0);
        }

        private void MoveInRelativeDirection(Vector4 movedirection)
        {
            UpdatePositionHelper(transform.position, transform.eulerAngles);
            movedirection = movedirection * Time.deltaTime * m_SpeedMultiplier;
            transform.position += m_CamPosHelper.forward * movedirection.y;
            transform.position += m_CamPosHelper.right * movedirection.x;
            transform.position += transform.forward * movedirection.z;
            Zoom(movedirection.w);
        }

        private void Zoom(float zoom)
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(
                new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
                transform.RotateAround(hit.point, new Vector3(0, 1, 0), zoom);
        }

        private void Update()
        {
            MoveInRelativeDirection(m_CameraMovemenManager.GetMovementSmoothed(0.2f));
        }
    }

}

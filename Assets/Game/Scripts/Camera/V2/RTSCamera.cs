using UnityEngine;
using AnotherRTS.Camera;

namespace AnotherRTS.Camera.WIP
{
    public class RTSCamera : MonoBehaviour
    {
        [SerializeField]
        private CamPosHelper m_PositionHelper;
        private CameraControlsManager m_ControlsManager;

        [SerializeField]
        private Transform m_CameraPlane;

        public void Start()
        {
            m_ControlsManager = new CameraControlsManager();
            m_ControlsManager.Add(new CameraControlKeyboard());
            m_ControlsManager.Add(new CameraControlsMouse(0.05f,1));
        }

        public void Update()
        {
            Vector4 moveVec = m_ControlsManager.GetMovementSmoothed(0.4f);



            m_PositionHelper.SetRotation(transform);
            m_PositionHelper.Translate(new Vector2(moveVec.x, moveVec.y));

            m_CameraPlane.Translate(0,moveVec.z*2,0);

            transform.eulerAngles = transform.localRotation.eulerAngles + new Vector3(0, moveVec.w, 0);
            transform.position = m_PositionHelper.GetCameraPosition(transform);
        }
    }
}


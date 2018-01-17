using UnityEngine;

namespace AnotherRTS.Camera.WIP
{
    [System.Serializable]
    public class CamPosHelper
    {
        [SerializeField] private Transform m_Object;
        [SerializeField] private Terrain m_terrain;
        [SerializeField] private LayerMask m_layermask;

        public void SetRotation(Transform CameraTransform)
        {
            Vector3 rotation = CameraTransform.eulerAngles;
            m_Object.transform.eulerAngles = new Vector3(0, rotation.y, 0);
        }

        public void Translate(Vector2 vec)
        {
            Vector3 pos = m_Object.position;
            pos += m_Object.right * vec.x;
            pos += m_Object.forward * vec.y;
            pos.y = m_terrain.SampleHeight(pos) + 0.1f;
            m_Object.position = pos;
        }

        public void MoveTo(Vector3 vec)
        {
            //vec.y = m_terrain.SampleHeight(vec) + 0.1f;
            m_Object.position = vec;
        }

        public Vector3 GetCameraPosition(Transform camera)
        {
            RaycastHit hitInfo;

            //Debug.DrawRay(m_Object.position,camera.forward);
            if (Physics.Raycast(m_Object.position, camera.forward * -1, out hitInfo, float.PositiveInfinity, m_layermask))
            {
                return hitInfo.point;
            }

            //
            return camera.position;
        }
    }
}


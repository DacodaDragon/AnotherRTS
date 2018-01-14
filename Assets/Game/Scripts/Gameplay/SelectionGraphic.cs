using UnityEngine;
using UnityEngine.UI;
using AnotherRTS.Management.RemappableInput;

namespace AnotherRTS.UI
{
    public delegate void SendSelectionRect(Rect r);
    public class SelectionGraphic: MonoBehaviour
    {
        private Vector2 m_pointStart;
        private Vector2 m_pointEnd;
        private Image image;
        private float Alpha = 0;
        private bool Enabled = false;
        public SendSelectionRect OnSelectionRelease;
        InputManager m_input;

        int DragSelectKey;

        private RectTransform m_rect;

        public void Awake()
        {
            DragSelectKey = InputManager.Instance.GetKeyID("drag select");
            m_rect = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            m_input = InputManager.Instance;
        }

        public void Enable(Vector2 start)
        {
            m_pointStart = start;
            m_pointEnd = start;
            Enabled = true;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Enabled)
            {
                m_pointEnd = Input.mousePosition;
                m_rect.position = m_pointStart;
                m_rect.sizeDelta = m_pointEnd - m_pointStart;

                if (Alpha != 1)
                {
                    Alpha += 1 * Time.deltaTime;
                    if (Alpha > 1)
                        Alpha = 1;
                    UpdateAlpha(Alpha);
                }

                if (m_input.GetKeyUp(DragSelectKey))
                {
                    Enabled = false;
                    OnSelectionRelease?.Invoke(new Rect(m_pointStart, m_pointEnd - m_pointStart));
                }
            }
            else
            {
                if (Alpha != 0)
                {
                    Alpha -= 1 * Time.deltaTime;
                    if (Alpha < 0)
                    {
                        Alpha = 0;
                        gameObject.SetActive(false);
                    }
                    UpdateAlpha(Alpha);
                }
            }
        }

        private void UpdateAlpha(float alpha)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }
}
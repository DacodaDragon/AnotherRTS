using UnityEngine;
using UnityEngine.UI;
using AnotherRTS.Management.RemappableInput;

namespace AnotherRTS.UI
{
    public delegate void SendSelectionRect(Rect r);
    public class SelectionGraphic: MonoBehaviour
    {
        private Vector2 m_mouseStart;
        private Vector2 m_mouseEnd;
        private Vector2 m_LU;
        private Vector2 m_RD;
        private Image image;
        private float Alpha = 0;
        private bool Enabled = false;
        public SendSelectionRect OnSelectionRelease;
        InputManager m_input;

        int DragSelectKey;

        private RectTransform m_rect;

        public void Start()
        {
            DragSelectKey = InputManager.Instance.GetKeyID("drag select");
            m_rect = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            m_input = InputManager.Instance;
        }

        public void Enable(Vector2 start)
        {
            m_mouseStart = start;
            m_mouseEnd = start;
            Enabled = true;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Enabled)
            {
                m_mouseEnd = Input.mousePosition;
                SetRectPosition(m_mouseStart.x, m_mouseStart.y, m_mouseEnd.x, m_mouseEnd.y, out m_LU, out m_RD);

                if (Alpha != 1)
                {
                    Alpha += 10 * Time.deltaTime;
                    if (Alpha > 1)
                        Alpha = 1;
                    UpdateAlpha(Alpha);
                }

                if (m_input.GetKeyUp(DragSelectKey))
                {
                    Enabled = false;
                    OnSelectionRelease?.Invoke(new Rect(m_LU, m_RD - m_LU));
                }
            }
            else
            {
                if (Alpha != 0)
                {
                    Alpha -= 10 * Time.deltaTime;
                    if (Alpha < 0)
                    {
                        Alpha = 0;
                        gameObject.SetActive(false);
                    }
                    UpdateAlpha(Alpha);
                }
            }
        }

        public void SetRectPosition(float x1, float y1, float x2, float y2, out Vector2 start, out Vector2 end)
        {
            if (x1 < x2)
            { start.x = x1; end.x = x2; }
            else
            { start.x = x2; end.x = x1; }
            
            if (y1 < y2)
            { start.y = y1; end.y = y2; }
            else
            { start.y = y2; end.y = y1 ; }

            m_rect.position = start;
            m_rect.sizeDelta = end - start;
        }

        private void UpdateAlpha(float alpha)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }
}
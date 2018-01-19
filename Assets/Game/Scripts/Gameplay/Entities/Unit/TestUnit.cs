using AnotherRTS.Gameplay.Entities;
using System;
using AnotherRTS.Util.Notification;
using UnityEngine;

namespace AnotherRTS.Gameplay.Entities.Units
{
    public class TestUnit : Unit
    {
        [SerializeField] Color m_SelectedColor;
        [SerializeField] Color m_DeselectedColor;
        [SerializeField] GameObject m_SelectedGraphic;
        
        public void Awake()
        {
            MovementController = GetComponent<IMovementController>();
            TaskManager = new StandardTaskManager(this);
            m_SelectedGraphic.SetActive(false);
            GetComponent<Renderer>().material.color = m_DeselectedColor;

        }

        public void Update()
        {
            TaskManager.TaskRun(this);
        }

        public override void OnEntitySelect()
        {
            base.OnEntitySelect();
            GetComponent<Renderer>().material.color = m_SelectedColor;
            m_SelectedGraphic.SetActive(true);
        }

        public override void OnEntityDeselect()
        {
            base.OnEntityDeselect();
            GetComponent<Renderer>().material.color = m_DeselectedColor;
            m_SelectedGraphic.SetActive(false);

        }

        public override void Destroy()
        {
            Destroy(gameObject);
        }
    }
}


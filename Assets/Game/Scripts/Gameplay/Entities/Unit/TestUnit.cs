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
        
        public void Awake()
        {
            MovementController = GetComponent<IMovementController>();
            TaskManager = new StandardTaskManager(this);
        }

        public void Update()
        {
            TaskManager.TaskRun(this);
        }

        public override void OnEntitySelect()
        {
            base.OnEntitySelect();
            GetComponent<Renderer>().material.color = m_SelectedColor;
        }

        public override void OnEntityDeselect()
        {
            base.OnEntityDeselect();
            GetComponent<Renderer>().material.color = m_DeselectedColor;

        }
    }
}


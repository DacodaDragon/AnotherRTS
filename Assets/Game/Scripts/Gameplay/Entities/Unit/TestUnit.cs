using AnotherRTS.Gameplay.Entities;
using System;
using AnotherRTS.Util.Notification;

namespace AnotherRTS.Gameplay.Entities.Unit
{
    public class TestUnit : Unit, ICommandableEntity<Unit>, IDamagable, IDestroyable, IDamageNotification<Unit>, IDeathNotification<Unit>, ISelectable
    {
        private ITaskManager<Unit> m_TaskManager;
        private float m_health;
        private float m_maxhealth;
        public ITaskManager<Unit> TaskManager{ get { return m_TaskManager; } }
        public float Health { get; }
        public float MaxHealth { get; }
        public event NotifyChange<Unit, float> OnDamageRecieved;
        public event Notify<Unit> OnDeath;

        private void Start()
        {
            m_TaskManager = new StandardTaskManager();
            m_TaskManager.TaskRun((Unit)this);
        }

        private void Update()
        {
            m_TaskManager.TaskRun((Unit)this);
        }

        public void Damage(float amount)
        {
            OnDamageRecieved.Invoke((Unit)this, amount);
        }

        public void Destroy()
        {
            OnDeath.Invoke((Unit)this);
        }
    }
}


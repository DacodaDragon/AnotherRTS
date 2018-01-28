using AnotherRTS.Util.Notification;
using Logger = BoneBox.Debug.Logger;
using UnityEngine.AI;
using System;

namespace AnotherRTS.Gameplay.Entities
{
    public abstract class Unit : Entity, ICommandableEntity<Unit>, IDamagable, IDestroyable, IDamageNotification<Unit>, IDeathNotification<Unit>, ISelectable, IMovable
    {
        private ITaskManager<Unit> m_TaskManager = null;
        private IMovementController m_MovementController = null;
        public ITaskManager<Unit> TaskManager { get { return m_TaskManager; } protected set { m_TaskManager = value; } }
        public IMovementController MovementController { get { return m_MovementController; } protected set { m_MovementController = value; } }
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }

        public event NotifyChange<Unit, float> OnDamageRecieved;
        public event Notify<Unit> OnDeath;

        void IDamagable.Damage(float amount) { Damage(amount); }
        void IDestroyable.Destroy() { OnDeath?.Invoke(this); Destroy(); }
        void ISelectable.OnEntityDeselect() { OnEntityDeselect(); }
        void ISelectable.OnEntitySelect() { OnEntitySelect(); }

        public virtual void Damage(float amount) { }
        public virtual void Destroy() { }
        public virtual void OnEntityDeselect() { }
        public virtual void OnEntitySelect() { }

        public virtual void Start()
        {
            Type = EType.Unit;
        }
    }
}
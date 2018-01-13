using AnotherRTS.Util.Notification;
using Logger = BoneBox.Debug.Logger;

namespace AnotherRTS.Gameplay.Entities
{
    public class Unit : Entity, ICommandableEntity<Unit>, IDamagable, IDestroyable, IDamageNotification<Unit>, IDeathNotification<Unit>, ISelectable
    {
        private ITaskManager<Unit> m_TaskManager = null;
        public ITaskManager<Unit> TaskManager { get { return m_TaskManager; } protected set { m_TaskManager = value; } }
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }
        public event NotifyChange<Unit, float> OnDamageRecieved;
        public event Notify<Unit> OnDeath;

        void IDamagable.Damage(float amount) { Damage(amount); }
        void IDestroyable.Destroy() { Destroy(); }
        void ISelectable.OnEntityDeselect() { OnEntityDeselect(); }
        void ISelectable.OnEntitySelect() { OnEntitySelect(); }

        public virtual void Damage(float amount) { Logger.Log(this, "Recieved Damage! " + amount); }
        public virtual void Destroy() { Logger.Log(this, "Got Destroyed!"); }
        public virtual void OnEntityDeselect() { Logger.Log(this, "Was Deselected!"); }
        public virtual void OnEntitySelect() { Logger.Log(this, "Was Selected!"); }

        private void Start()
        {
            Type = EType.Unit;
        }
    }
}
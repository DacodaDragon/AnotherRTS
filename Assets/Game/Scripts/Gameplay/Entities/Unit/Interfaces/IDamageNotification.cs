using AnotherRTS.Util.Notification;

namespace AnotherRTS.Gameplay.Entities
{
    public interface IDamageNotification<context>
    {
        event NotifyChange<context, float> OnDamageRecieved;
    }
}


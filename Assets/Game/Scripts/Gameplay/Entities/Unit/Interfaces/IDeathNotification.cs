using AnotherRTS.Util.Notification;

namespace AnotherRTS.Gameplay.Entities
{
    public interface IDeathNotification<context>
    {
        event Notify<context> OnDeath;
    }
}


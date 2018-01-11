using AnotherRTS.Util.Notification;

namespace AnotherRTS.Gameplay.Entities
{
    public interface IDestroyNotification<Context>
    {
        event Notify<Context> OnDestroyed;
    }
}


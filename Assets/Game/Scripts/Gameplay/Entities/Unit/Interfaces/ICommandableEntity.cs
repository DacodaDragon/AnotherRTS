// TODO: Remove this after stan is done

namespace AnotherRTS.Gameplay.Entities
{
    public interface ICommandableEntity<Context>
    {
        ITaskManager<Context> TaskManager { get; }
    }
}


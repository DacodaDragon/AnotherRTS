// TODO: Remove this after stan is done

namespace AnotherRTS.Gameplay.Entities
{
    public interface ICommandableEntity<Context>
    {
        ITaskable<Context> TaskManager { get; }
    }
}


// TODO: Remove this after stan is done

namespace AnotherRTS.Gameplay.Entities
{
    public interface ITask<Context>
    {
        string TaskName { get; }
        ETaskRequirement[] TaskRequirements { get; }
        void RunTask(Context context);
    }
}


// TODO: Remove this after stan is done

namespace AnotherRTS.Gameplay.Entities
{
    public interface ITaskManager<context>
    {
        void TaskIsCompatible(params ITask<context>[] Tasks);
        void TaskIsCompatible(params ETaskRequirement[] Requirements);
        void TaskAdd(ITask<context> Task);
        void TaskRemove(ITask<context> Task);
        void TaskForce(ITask<context> Task);
        void TaskRun(context task);
        void TaskClearAll();
        void TaskAssign(ITask<context> Task);
    }
}


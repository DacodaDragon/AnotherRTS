// TODO: Remove this after stan is done

namespace AnotherRTS.Gameplay.Entities
{
    public interface ITaskManager<context>
    {
        bool TaskIsCompatible(params ITask<context>[] Tasks);
        bool TaskIsCompatible(params ETaskRequirement[] Requirements);
        void SetNewTaskRequirements(params ETaskRequirement[] Requirements);
        void TaskAdd(ITask<context> Task);
        void TaskRemove(ITask<context> Task);
        void TaskForce(ITask<context> Task);
        void TaskAssign(ITask<context> Task);
        void TaskRun(context context);
        void TaskNext(context context);
        void TaskClearAll();
    }
}


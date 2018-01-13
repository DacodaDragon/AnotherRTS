using System.Collections.Generic;
using System.Linq;

namespace AnotherRTS.Gameplay.Entities.Unit
{
    public class StandardTaskManager : ITaskManager<Unit>
    {
        static readonly ETaskRequirement[] TaskRequirements =
        {
            ETaskRequirement.CanMove,
            ETaskRequirement.CanAttackGround
        };

        List<ITask<Unit>> Tasks;

        public void TaskAdd(ITask<Unit> Task)
        {
            Tasks.Add(Task);
        }

        public void TaskAssign(ITask<Unit> Task)
        {
            TaskClearAll();
            Tasks.Add(Task);
        }

        public void TaskClearAll()
        {
            Tasks.Clear();
        }

        public void TaskForce(ITask<Unit> Task)
        {
            // TODO: Find a faster way to perform this
            List<ITask<Unit>> newTasks = new List<ITask<Unit>>(Tasks.Count + 1);
            newTasks.Add(Task);
            newTasks.AddRange(Tasks);
            Tasks = newTasks;

        }

        public bool TaskIsCompatible(params ITask<Unit>[] Tasks)
        {
            // TODO: Can be optimzed when we have the time!
            return Tasks.Where(x => x.TaskRequirements.Where(y => TaskRequirements.Contains(y)).Count() == x.TaskRequirements.Length).Count() == Tasks.Length;
        }

        public bool TaskIsCompatible(params ETaskRequirement[] Requirements)
        {
            return Requirements.Where(
                       x => TaskRequirements.Contains(x)
                   ).Count() == Requirements.Length;
        }

        public void TaskRemove(ITask<Unit> Task)
        {
            Tasks.Remove(Task);
        }

        public void TaskRun(Unit context)
        {
            if (Tasks.Count > 0)
                Tasks[0].RunTask(context);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnotherRTS.Gameplay.Entities.Units
{
    public class StandardTaskManager : ITaskManager<Unit>
    {
        List<ITask<Unit>> Tasks;
        Unit contextUnit;

        private ETaskRequirement[] m_TaskRequirements =
        {
            ETaskRequirement.CanMove,
            ETaskRequirement.CanAttackGround
        };
        public ETaskRequirement[] TaskRequirements { get { return m_TaskRequirements; } }

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
            Tasks.Insert(0, Task);
            Tasks[0].StartTask(contextUnit);
            //// TODO: Find a faster way to perform this
            //List<ITask<Unit>> newTasks = new List<ITask<Unit>>(Tasks.Count + 1);
            //newTasks.Add(Task);
            //newTasks.AddRange(Tasks);
            //Tasks = newTasks;
        }

        public bool TaskIsCompatible(params ITask<Unit>[] Tasks)
        {
            // TODO: Unlazyfy
            return Tasks.Where(
                   x => x.TaskRequirements.Where(
                   y => m_TaskRequirements.Contains(y)
                   ).Count() == x.TaskRequirements.Length
                   ).Count() == Tasks.Length;
        }

        public bool TaskIsCompatible(params ETaskRequirement[] Requirements)
        {
            return Requirements.Where(
                       x => m_TaskRequirements.Contains(x)
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

        public void SetNewTaskRequirements(params ETaskRequirement[] Requirements)
        {
            m_TaskRequirements = Requirements;
        }

        public void TaskNext(Unit context)
        {
            Tasks[0].EndTask(context);
            Tasks.RemoveAt(0);
            if (Tasks.Count > 0)
                Tasks[0].StartTask(context);
        }

        public StandardTaskManager(Unit context)
        {
            Tasks = new List<ITask<Unit>>();
            contextUnit = context;
        }
    }
}
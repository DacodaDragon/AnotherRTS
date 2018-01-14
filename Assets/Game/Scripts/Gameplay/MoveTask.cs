
using UnityEngine;

using BoneBox.Core;
using AnotherRTS.Gameplay.Entities;

namespace AnotherRTS.Gameplay
{
    public partial class Selector : Singleton<Selector>
    {
        public class MoveTask : ITask<Unit>
        {
            public string TaskName { get { return "Move Unit"; } }

            private Vector3 targetposition;

            public ETaskRequirement[] TaskRequirements
            {
                get
                {
                    return new ETaskRequirement[]
                    { ETaskRequirement.CanMove };
                }
            }

            public void EndTask(Unit context)
            {

            }

            public void RunTask(Unit context)
            {

            }

            public void StartTask(Unit context)
            {
                ((IMovable)context)
                    .MovementController
                    .MoveTowards(targetposition);
            }

            public MoveTask(Vector3 position)
            {
                this.targetposition = position;
            }
        }
    }
}
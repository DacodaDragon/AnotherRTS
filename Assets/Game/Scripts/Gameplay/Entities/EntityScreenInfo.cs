using UnityEngine;

namespace AnotherRTS.Gameplay.Entities.Units
{
    public struct EntityScreenInfo<ContextType>
    {
        public readonly Vector2 position;
        public readonly ContextType context;

        public EntityScreenInfo(Vector2 pos, ContextType context )
        {
            this.position = pos;
            this.context = context;
        }
    }
}
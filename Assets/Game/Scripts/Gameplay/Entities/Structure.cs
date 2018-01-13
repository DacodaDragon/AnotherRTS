using System;

namespace AnotherRTS.Gameplay.Entities
{
    public class Structure : Entity, ISelectable
    {
        public void OnEntityDeselect()
        {
            throw new NotImplementedException();
        }

        public void OnEntitySelect()
        {
            throw new NotImplementedException();
        }
    }
}
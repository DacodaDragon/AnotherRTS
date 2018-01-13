
namespace AnotherRTS.Gameplay.Entities
{
	public interface ISelectable
	{
        void OnEntitySelect();      // gets called when the entity is selected
        void OnEntityDeselect();    // gets called when the entity is deselected
	}
}
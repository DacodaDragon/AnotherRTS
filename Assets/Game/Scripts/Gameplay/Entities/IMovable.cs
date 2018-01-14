namespace AnotherRTS.Gameplay.Entities
{
    public interface IMovable
    {
        IMovementController MovementController { get; }
    }
}
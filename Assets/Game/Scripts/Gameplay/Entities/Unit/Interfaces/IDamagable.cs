namespace AnotherRTS.Gameplay.Entities
{
    public interface IDamagable
    {
        float Health { get; }
        float MaxHealth { get; }
        void Damage(float amount);
    }
}


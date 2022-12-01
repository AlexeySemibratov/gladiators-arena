namespace GladiatorsArena.Heroes
{
    public record Damage(
        int DamageAmount,
        DamageType Type);

    public enum DamageType
    {
        Physical,
        Magical
    }
}

namespace GladiatorsArena.Heroes
{
    /// <summary>
    /// Воин.
    /// Урон от базовой атаки: физический.
    /// Способность - Месть. 
    /// Получая смертельный урон, воин отсрочивает свой исход и восстанавливает 15%
    /// от своего максимального запаса здоровья, а также увеличивает свой базовый урон от атаки на количество урона, равное значению смертельного урона.
    /// Способность может сработать только один раз.
    /// </summary>
    internal class Warrior : Hero, IWarrior
    {
        public event Action<Hero> RevengeActivated;

        private Revenge _revenge;

        public Warrior(string name, int maxHP, Damage baseAttackDamage) : base(name, maxHP, baseAttackDamage)
        {
            _revenge = new Revenge(this);
        }

        public override void RecieveDamage(Hero target, Damage damage)
        {
            base.RecieveDamage(target, damage);
            if (_revenge.ShouldActivate())
            {
                RevengeActivated?.Invoke(this);
                _revenge.Activate(damage);
            }
        }
    }
}

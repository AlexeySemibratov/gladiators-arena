using GladiatorsArena.DamageData;

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

        public Warrior(string name, int maxHP, Damage baseAttackDamage) : base(name, maxHP, baseAttackDamage, HeroType.Warrior)
        {
            _revenge = new Revenge(this);
        }

        public override void ReceiveDamage(IDamageTarget dealer, Damage damage)
        {
            base.ReceiveDamage(dealer, damage);

            if (_revenge.ShouldActivate())
            {
                RevengeActivated?.Invoke(this);
                _revenge.Activate();
                ApplyRevengeBuffs(damage);
            }
        }

        private void ApplyRevengeBuffs(Damage incomingDamage)
        {
            Heal(_revenge.GetHPRestorationAmount());
            SetDamage(_revenge.GetBuffedDamage(incomingDamage));
        }
    }
}

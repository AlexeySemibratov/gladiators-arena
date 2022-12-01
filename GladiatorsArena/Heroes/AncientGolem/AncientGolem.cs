namespace GladiatorsArena.Heroes.AncientGolem
{
    /// <summary>
    /// Древний Голем.
    /// Урон от базовой атаки: физический.
    /// Способность - Каменная Форма. 
    /// Голем создан из магического камня, который отражает 15% входящего физического урона
    /// обратно противнику, может сработать только от одной атаки за раунд. 
    /// Нестабильность древнего камня приводит к тому, что каждая атака голема наносит
    /// случайный урон c разницей в 50% (как в большую так и в меньшую сторону) от базового урона.
    /// </summary>
    internal class AncientGolem : Hero, IAncientGolem
    {
        public event Action<Hero> StoneFormReflectDamage;

        private StoneForm _stoneForm;

        public AncientGolem(string name, int maxHP, Damage baseAttackDamage) : base(name, maxHP, baseAttackDamage)
        {
            _stoneForm = new StoneForm(this);
        }

        public override void RecieveDamage(Hero target, Damage damage)
        {
            base.RecieveDamage(target, damage);

            if (_stoneForm.ShouldReflectDamage(damage))
            {
                StoneFormReflectDamage?.Invoke(this);
                _stoneForm.ReflectDamage(target, damage);
            }
        }

        protected override Damage GetOutgoingDamage()
        {
            return _stoneForm.CalculateDispersionDamage(BaseAttackDamage);
        }

        public override void BeforeRound()
        {
            base.BeforeRound();
            _stoneForm.BeforeNewRound();
        }
    }
}

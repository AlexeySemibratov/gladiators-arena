namespace GladiatorsArena.Heroes
{
    /// <summary>
    /// Маг.
    /// Урон от базовой атаки: магический.
    /// В конце каждого раунда восстанавливает 25 маны.
    /// Способность - Ледяная форма (Расход маны = 100). 
    /// Если достаточно маны, то в начале раунда накладывет на себя 
    /// эффект ледяной формы, который удвоит урон от следующей атаки и наложит на заклинателя
    /// ледяной щит, поглощающий любой входящий урон от следующей атаки противника в полном объеме.
    /// </summary>
    internal class Mage : Hero, IMage
    {
        public const int ManaRegenerationPerRound = 25;

        public event Action<Hero> FrostFormActivated;

        public int MaxMana { get; private set; }
        public int CurrentMana { get; private set; }

        private FrostForm _frostForm;

        public Mage(
            string name,
            int maxHP,
            Damage baseAttackDamage,
            int maxMana) : base(name, maxHP, baseAttackDamage)
        {
            MaxMana = maxMana;
            CurrentMana = 0;

            _frostForm = new FrostForm(this);
        }

        protected override Damage GetOutgoingDamage()
        {
            var damage = _frostForm.GetBuffedDamage(base.GetOutgoingDamage());
            return damage;
        }

        public override void RecieveDamage(Hero target, Damage damage)
        {
            var actualDamage = _frostForm.ReduceDamage(damage);
            base.RecieveDamage(target, actualDamage);
        }

        public override void BeforeRound()
        {
            base.BeforeRound();
            if (_frostForm.IsEnoughMana())
            {
                FrostFormActivated?.Invoke(this);
                _frostForm.ActivateAbility();
            }
        }

        public override void AfterRound()
        {
            base.AfterRound();
            AddMana(ManaRegenerationPerRound);
        }

        public void AddMana(int manaToAdd)
        {
            CurrentMana = Math.Clamp(CurrentMana + manaToAdd, 0, MaxMana);
        }

        public void RemoveMana(int manaToRemove)
        {
            CurrentMana = Math.Clamp(CurrentMana - manaToRemove, 0, MaxMana);
        }
    }
}

using GladiatorsArena.DamageData;
using static System.Net.Mime.MediaTypeNames;

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
            int maxMana) : base(name, maxHP, baseAttackDamage, HeroType.Mage)
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

        public override void ReceiveDamage(IDamageTarget dealer, Damage damage)
        {
            var actualDamage = _frostForm.ReduceDamage(damage);
            base.ReceiveDamage(dealer, actualDamage);
        }

        public override void OnRoundStarted()
        {
            base.OnRoundStarted();

            if (IsEnoughManaForFrostForm())
            {
                FrostFormActivated?.Invoke(this);
                _frostForm.ActivateAbility();
                RemoveMana(_frostForm.GetManaCost());
            }
        }

        public bool IsEnoughManaForFrostForm()
        {
            return CurrentMana >= _frostForm.GetManaCost();
        }

        public override void OnRoundFinished()
        {
            base.OnRoundFinished();
            AddMana(ManaRegenerationPerRound);
        }

        private void AddMana(int manaToAdd)
        {
            CurrentMana = Math.Clamp(CurrentMana + manaToAdd, 0, MaxMana);
        }

        private void RemoveMana(int manaToRemove)
        {
            CurrentMana = Math.Clamp(CurrentMana - manaToRemove, 0, MaxMana);
        }
    }
}

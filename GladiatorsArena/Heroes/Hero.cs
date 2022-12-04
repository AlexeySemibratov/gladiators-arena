using GladiatorsArena.ArenaModule;
using GladiatorsArena.DamageData;

namespace GladiatorsArena.Heroes
{
    internal abstract class Hero : IArenaFighter, IDamageTarget
    {
        public delegate void DamageDealedEventHandler(Hero Target, Damage DamageSource);
        public delegate void DamageReceivedEventHandler(Hero Target, Damage DamageSource);
        public delegate void HealReceivedEventHandler(Hero Target, int HealAmount);
        public delegate void DamageChangedEventHandler(Hero Target, Damage OldValue, Damage NewValue);

        public event DamageDealedEventHandler DamageDealed;
        public event DamageReceivedEventHandler DamageReceived;
        public event HealReceivedEventHandler HealReceived;
        public event DamageChangedEventHandler DamageChanged;

        public string Name { get; private set; }

        public HeroType HeroType { get; private set; }

        public Damage BaseAttackDamage { get; private set; }

        public int MaxHP { get; private set; }

        public int CurrentHP { get; private set; }

        public Hero(string name, int maxHP, Damage baseAttackDamage, HeroType heroType)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            BaseAttackDamage = baseAttackDamage;
            HeroType = heroType;
        }

        public bool CheckIsDead()
        {
            return CurrentHP <= 0;
        }

        public virtual void DealDamage(IDamageTarget receiver)
        {
            Damage damage = GetOutgoingDamage();
            DamageDealed?.Invoke(this, damage);
            receiver.ReceiveDamage(this, damage);
        }

        public virtual void ReceiveDamage(IDamageTarget dealer, Damage damage)
        {
            CurrentHP = Math.Clamp(CurrentHP - damage.DamageAmount, 0, MaxHP);
            DamageReceived?.Invoke(this, damage);
        }

        public virtual void OnRoundStarted()
        {
        }

        public virtual void PerformRound(IDamageTarget target)
        {
            DealDamage(target);
        }

        public virtual void OnRoundFinished()
        {
        }

        protected virtual Damage GetOutgoingDamage()
        {
            return BaseAttackDamage;
        }

        protected virtual void SetDamage(Damage newDamage)
        {
            DamageChanged?.Invoke(this, BaseAttackDamage, newDamage);
            BaseAttackDamage = newDamage;
        }

        protected virtual void Heal(int healAmount)
        {
            CurrentHP = Math.Clamp(CurrentHP + healAmount, 0, MaxHP);

            HealReceived?.Invoke(this, healAmount);
        }
    }
}

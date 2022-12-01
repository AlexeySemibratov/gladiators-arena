using GladiatorsArena.Arena;

namespace GladiatorsArena.Heroes
{
    internal abstract class Hero : IArenaEntity<Hero>
    {
        public delegate void DamageDealedEventHandler(Hero Dealer, Hero Target, Damage DamageSource);
        public delegate void DamageReceivedEventHandler(Hero Dealer, Hero Target, Damage DamageSource);
        public delegate void HealReceivedEventHandler(Hero Target, int HealAmount);
        public delegate void DamageChangedEventHandler(Hero Target, Damage OldValue, Damage NewValue);

        public event DamageDealedEventHandler DamageDealed;
        public event DamageReceivedEventHandler DamageReceived;
        public event HealReceivedEventHandler HealReceived;
        public event DamageChangedEventHandler DamageChanged;

        public string Name { get; private set; }
        public int MaxHP { get; private set; } 
        public int CurrentHP { get; private set; }

        public Damage BaseAttackDamage { get; private set; }
        public Hero Entity => this; 

        private HeroEvents _events = new HeroEvents();

        public Hero(string name, int maxHP, Damage baseAttackDamage)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            BaseAttackDamage = baseAttackDamage;
        }

        protected virtual Damage GetOutgoingDamage()
        {
            return BaseAttackDamage;
        }

        public virtual void DealDamage(Hero target)
        {
            Damage damage = GetOutgoingDamage();
            DamageDealed?.Invoke(this, target, damage);
            target.RecieveDamage(this, damage);
        }

        public virtual void RecieveDamage(Hero target, Damage damage)
        {
            CurrentHP = Math.Clamp(CurrentHP - damage.DamageAmount, 0, MaxHP);
            DamageReceived?.Invoke(target, this, damage);
        }

        public virtual void SetDamage(Damage newDamage)
        {
            DamageChanged?.Invoke(this, BaseAttackDamage, newDamage);
            BaseAttackDamage = newDamage;
        }

        public virtual void Heal(int healAmount)
        {
            CurrentHP = Math.Clamp(CurrentHP + healAmount, 0, MaxHP);

            HealReceived?.Invoke(this, healAmount);
        }

        public bool IsDead()
        {
            return CurrentHP <= 0;
        }

        public virtual void BeforeRound()
        {
        }

        public virtual void OnRound(Hero target)
        {
            DealDamage(target);
        }

        public virtual void AfterRound()
        {
        }
    }
}

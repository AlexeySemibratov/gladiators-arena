namespace GladiatorsArena.Heroes
{
    internal class HeroEvents
    {
        public delegate void DamageDealedEventHandler(Hero Dealer, Hero Target, Damage DamageSource);
        public delegate void DamageRecivedEventHandler(Hero Dealer, Hero Target, Damage DamageSource);
        public delegate void HealRecivedEventHandler(Hero Target, int HealAmount);

        public event DamageDealedEventHandler DamageDealed;
        public event DamageRecivedEventHandler DamageRecived;
        public event HealRecivedEventHandler HealRecived;

        public HeroEvents()
        {
        }

        public void CallDamageDelaedEvent(Hero dealer, Hero target, Damage damage)
        {

        }
    }
}

using GladiatorsArena.DamageData;
using GladiatorsArena.Heroes;
using GladiatorsArena.Heroes.AncientGolem;
using GladiatorsArena.Heroes.ChaosKnight;
using GladiatorsArena.Heroes.Vampire;

namespace GladiatorsArena.ArenaModule
{
    internal class HeroFactory
    {
        private readonly Damage _warDamage = new Damage(20, DamageType.Physical);
        private readonly Damage _chaosDamage = new Damage(15, DamageType.Physical);
        private readonly Damage _mageDamage = new Damage(25, DamageType.Magical);
        private readonly Damage _vampireDamage = new Damage(15, DamageType.Physical);
        private readonly Damage _golemDamage = new Damage(20, DamageType.Physical);

        public HeroFactory()
        {
        }

        public Hero CreateHeroByType(HeroType type, string name)
        {
            return type switch
            {
                HeroType.Warrior => CreateWarrior(name),
                HeroType.Mage => CreateMage(name),
                HeroType.AncientGolem => CreateAncientGolem(name),
                HeroType.Vampire => CreateVampire(name),
                HeroType.ChaosKnight => CreateChaosKnight(name),
            };      
        }

        private Hero CreateWarrior(string name)
        {
            return new Warrior(name, 180, _warDamage);
        }

        private Hero CreateMage(string name)
        {
            return new Mage(name, 120, _mageDamage, 100);
        }

        private Hero CreateAncientGolem(string name)
        {
            return new AncientGolem(name, 200, _golemDamage);
        }

        private Hero CreateChaosKnight(string name)
        {
            return new ChaosKnight(name, 160, _chaosDamage);
        }

        private Hero CreateVampire(string name)
        {
            return new Vampire(name, 140, _vampireDamage);
        }
    }
}

using GladiatorsArena.Heroes;
using GladiatorsArena.Heroes.AncientGolem;
using GladiatorsArena.Heroes.ChaosKnight;
using GladiatorsArena.Heroes.Vampire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorsArena.Arena
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

        public Hero CreateWarrior(string name)
        {
            return new Warrior(name, 180, _warDamage);
        }

        public Hero CreateMage(string name)
        {
            return new Mage(name, 120, _mageDamage, 100);
        }

        public Hero CreateAncientGolem(string name)
        {
            return new AncientGolem(name, 200, _golemDamage);
        }

        public Hero CreateChaosKnight(string name)
        {
            return new ChaosKnight(name, 160, _chaosDamage);
        }

        public Hero CreateVampire(string name)
        {
            return new Vampire(name, 140, _vampireDamage);
        }
    }
}

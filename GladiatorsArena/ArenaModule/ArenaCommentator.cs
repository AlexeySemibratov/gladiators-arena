using GladiatorsArena.DamageData;
using GladiatorsArena.Heroes;
using GladiatorsArena.Presentation;
using System.Drawing;

namespace GladiatorsArena.ArenaModule
{
    internal class ArenaCommentator
    {

        private Hero _firstHero;
        private Hero _secondHero;

        private Dictionary<DamageType, string> _damageTypesNames = new Dictionary<DamageType, string>
        {
            { DamageType.Physical, "Физического" },
            { DamageType.Magical, "Магического" }
        };

        public ArenaCommentator(Hero firstHero, Hero secondHero)
        {
            _firstHero = firstHero;
            _secondHero = secondHero;

            RegisterHeroEvents(_firstHero);
            RegisterHeroEvents(_secondHero);
        }

        private void RegisterHeroEvents(Hero hero)
        {
            hero.DamageDealed += CommentDamageDealedEvent;
            hero.DamageReceived += CommentDamageReceivedEvent;
            hero.HealReceived += CommentHealReceivedEvent;
            hero.DamageChanged += CommentDamageChangedEvent; 

            switch (hero)
            {
                case IWarrior warrior:
                    warrior.RevengeActivated += CommentWarriorRevengeActivatedEvent;
                    break;
                case IMage mage:
                    mage.FrostFormActivated += CommentFrostFormActivated;
                    break;
                case IChaosKnight chaos:
                    chaos.ChaosCopySummoned += CommentChaosCopySummoned;
                    break;
                case IAncientGolem golem:
                    golem.StoneFormReflectDamage += CommentGolemStoneFormReflectDamage;
                    break;
                default:
                    break;
            }
        }

        public void CommentBattleStart()
        {
            Console.WriteLine("Битва начинается!".Colored(Colors.Orange));
        }

        public void CommentRoundStart(int roundNumber)
        {
            Console.WriteLine("Раунд {0}!".Colored(Colors.Orange), roundNumber);
            Console.WriteLine();
        }

        public void CommentBattleEnd()
        {
            Console.WriteLine("Битва завершилась!".Colored(Colors.Orange));
            CommentBattleResult();
        }

        private void CommentBattleResult()
        {
            if (_firstHero.CheckIsDead() && _secondHero.CheckIsDead())
            {
                Console.WriteLine("Участники не смогли выявить победителя!".Colored(Colors.Orange));
            }
            else if (_firstHero.CheckIsDead())
            {
                Console.WriteLine("Победил второй участник!".Colored(Colors.Orange));
            }
            else
            {
                Console.WriteLine("Победил первый участник!".Colored(Colors.Orange));
            }
        }

        public void CommentBeforeRoundStarted()
        {
            CommentHeroStatus(_firstHero);
            CommentHeroStatus(_secondHero);
        }

        public void CommentOnRoundStarted()
        {
            Console.WriteLine();
        }

        public void CommentRoundEnded()
        {
            Console.WriteLine();
        }

        private void CommentHeroStatus(Hero hero)
        {
            PrintHeroStatus(hero);

            if (hero is IMage mage)
            {
                PrintMageStatus(mage);
            };

            Console.WriteLine();
        }

        private void PrintMageStatus(IMage mage)
        {
            var text = string.Format("Mана: {0}", mage.CurrentMana);
            Console.Write(text);
        }

        private void PrintHeroStatus(Hero hero)
        {
            string text = string.Format("{0}. Здоровье: {1} Урон: {2} ", GetHeroColoredName(hero), hero.CurrentHP, hero.BaseAttackDamage.DamageAmount);
            Console.Write(text);
        }

        private void CommentGolemStoneFormReflectDamage(Hero hero)
        {
            var text = string.Format("{0} отражает часть полученного урон!", GetHeroColoredName(hero));
            Console.WriteLine(text);
        }

        private void CommentChaosCopySummoned(Hero hero)
        {
            var text = string.Format("{0} призывает хаос-копию!", GetHeroColoredName(hero));
            Console.WriteLine(text);
        }

        private void CommentFrostFormActivated(Hero hero)
        {
            var text = string.Format("{0} активирует ледяную форму!", GetHeroColoredName(hero));
            Console.WriteLine(text);
        }

        private void CommentDamageDealedEvent(Hero dealer, Damage damageSource)
        {
            var text = string.Format("{0} атакует!", GetHeroColoredName(dealer));
            Console.WriteLine(text);
        }

        private void CommentDamageReceivedEvent(Hero target, Damage damageSource)
        {
            string text;

            if (damageSource.DamageAmount > 0)
            {
                text = string.Format("{0} получает {1} ({2}) урона!", GetHeroColoredName(target), damageSource.DamageAmount, GetDamageTypeName(damageSource.Type));
            }
            else
            {
                text = string.Format("{0} избегает входящий урон!", GetHeroColoredName(target));
            }

            Console.WriteLine(text);
        }

        private void CommentHealReceivedEvent(Hero target, int healAmount)
        {
            var text = string.Format("{0} излечивается на {1} единиц здоровья!", GetHeroColoredName(target), healAmount);
            Console.WriteLine(text);
        }

        private void CommentDamageChangedEvent(Hero target, Damage oldValue, Damage newValue)
        {
            bool wasDamageIncreased = newValue.DamageAmount >= oldValue.DamageAmount;
            int damageChangedAmount = Math.Abs(newValue.DamageAmount - oldValue.DamageAmount);

            string text;

            if (wasDamageIncreased)
            {
                text = string.Format("{0} увеличвает свой урон на {1} единиц!", GetHeroColoredName(target), damageChangedAmount);
            }
            else
            {
                text = string.Format("{0} получает уменьшение урона на {1} единиц!", GetHeroColoredName(target), damageChangedAmount);
            }

            Console.WriteLine(text);
        }

        private void CommentWarriorRevengeActivatedEvent(Hero hero)
        {
            var text = string.Format("{0} отвергает смерть!", GetHeroColoredName(hero));
            Console.WriteLine(text);
        }


        private string GetDamageTypeName(DamageType type)
        {
            return _damageTypesNames[type];
        }

        private string GetHeroColoredName(Hero hero)
        {
            Color color = hero.HeroType.GetHeroTypeColor();
            return hero.Name.Colored(color);
        }

    }
}

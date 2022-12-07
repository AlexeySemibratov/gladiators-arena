using GladiatorsArena.Heroes;
using System.Drawing;

namespace GladiatorsArena.Presentation
{
    public static class HeroTypeColorsExtension
    {
        private static readonly Dictionary<HeroType, Color> HeroTypeColors = new Dictionary<HeroType, Color>
        {
            { HeroType.Warrior, Colors.Gray },
            { HeroType.Mage, Colors.Blue },
            { HeroType.Vampire, Colors.Olive },
            { HeroType.ChaosKnight, Colors.Red },
            { HeroType.AncientGolem, Colors.Brown },
        };

        public static Color GetHeroTypeColor(this HeroType type)
        {
            return HeroTypeColors.GetValueOrDefault(type, Colors.Gray);
        }
    }
}

using GladiatorsArena.Heroes;
using System.Drawing;

namespace GladiatorsArena.Presentation
{
    internal static class ColoredStringExtension
    {
        private static readonly string ResetCode = "\u001b[0m";

        private static readonly Dictionary<HeroType, Color> HeroTypeColors = new Dictionary<HeroType, Color>
        {
            { HeroType.Warrior, Colors.Gray },
            { HeroType.Mage, Colors.Blue },
            { HeroType.Vampire, Colors.Olive },
            { HeroType.ChaosKnight, Colors.Red },
            { HeroType.AncientGolem, Colors.Brown },
        };

        public static string Colored(this string self, Color color)
        {
            return string.Format("{0}{1}{2}", GetColorAnsiCode(color), self, ResetCode);
        }

        public static Color GetHeroTypeColor(this HeroType type)
        {
            return HeroTypeColors.GetValueOrDefault(type, Colors.Gray);
        }

        private static string GetColorAnsiCode(Color color)
        {
            return string.Format("\u001b[38;2;{0};{1};{2}m", color.R, color.G, color.B);
        }
    }
}

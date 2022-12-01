using System.Drawing;

namespace GladiatorsArena.Presentation
{
    internal static class ColoredStringExtension
    {
        private static readonly string ResetCode = "\u001b[0m";

        public static string Colored(this string self, Color color)
        {
            return string.Format("{0}{1}{2}", GetColorAnsiCode(color), self, ResetCode);
        }

        private static string GetColorAnsiCode(Color color)
        {
            return string.Format("\u001b[38;2;{0};{1};{2}m", color.R, color.G, color.B);
        }
    }
}

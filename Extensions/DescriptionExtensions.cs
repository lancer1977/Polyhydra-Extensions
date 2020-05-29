namespace PolyhydraGames.Extensions
{
    public static class DescriptionExtensions
    {
        public static string PrefixMod(this int modifier)
        {
            return ((modifier >= 0) ? "+" : "") + modifier;
        }
    }
}
namespace PolyhydraGames.Extensions;

public static class DescriptionExtensions
{
    public static string PrefixMod(this int modifier) => modifier >= 0 ? "+" + modifier : modifier.ToString();
}
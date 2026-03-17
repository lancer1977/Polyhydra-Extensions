using System;

namespace PolyhydraGames.Extensions;

public static class NumericExtensions
{
    /// <summary>
    /// Clamps a value between min and max.
    /// </summary>
    public static double Clamp(this double self, double min, double max)
    {
        return Math.Min(max, Math.Max(self, min));
    }

    /// <summary>
    /// Clamps a value between min and max.
    /// </summary>
    public static int Clamp(this int self, int min, int max)
    {
        return Math.Min(max, Math.Max(self, min));
    }

    public static int Squared(this int val)
    {
        return val * val;
    }
}
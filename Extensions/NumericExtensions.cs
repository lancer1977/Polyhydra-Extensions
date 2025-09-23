using System;

namespace PolyhydraGames.Extensions;

public static class NumericExtensions
{
    /// <summary>
    /// Clamps the specified self.
    /// </summary>
    /// <param name="self">The self.</param>
    /// <param name="min">The minimum.</param>
    /// <param name="max">The maximum.</param>
    /// <returns>System.Double.</returns>
    public static double Clamp(this double self, double min, double max)
    {
        return Math.Min(max, Math.Max(self, min));
    }

    /// <summary>
    /// Clamps the specified self.
    /// </summary>
    /// <param name="self">The self.</param>
    /// <param name="min">The minimum.</param>
    /// <param name="max">The maximum.</param>
    /// <returns>System.Int32.</returns>
    public static int Clamp(this int self, int min, int max)
    {
        return Math.Min(max, Math.Max(self, min));
    }

    public static int Squared(this int val)
    {
        return val * val;
    }
}
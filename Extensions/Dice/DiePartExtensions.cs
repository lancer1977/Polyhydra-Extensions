using System.Collections.Generic;
using System.Linq;

namespace PolyhydraGames.Extensions.Dice;

public static class DiePartExtensions
{
    public static int Result(this IEnumerable<DiePart> items, out string stringOut)
    {
        var intResult = 0;
        var stringResult = "";
        foreach (var item in items)
        {
            var localResult = DiceRoll.RollNDie(item.Die);
            intResult += item.Negative ? (-1*localResult) : localResult; 
            stringResult += (item.Negative ? " - " : 
                                (string.IsNullOrEmpty(stringResult)? "": " + ")
                            )
                            + localResult;
        }
       
        stringOut =  $"Total: {intResult} DieRolls: {stringResult}" ;
        return intResult;
    }
    public static List<DiePart> ToDieParts(this string value)
    {
        var items = new List<DiePart>();
        if (string.IsNullOrEmpty(value)) return items;
        value = value.Replace(" ","");
        var firstSplit = value.Split('+');
        var positivesOnly = firstSplit.Where(i => !i.Contains("-"));
        var negativesAlso = firstSplit.Where(i => i.Contains("-")).ToArray();
        items.AddRange(positivesOnly.Select(i=>new DiePart() {Die = i}));
        if (!negativesAlso.Any()) return items;
        foreach (var item in negativesAlso)
        {
            var split = item.Split('-');
            items.AddRange(split.Select((t, x) => new DiePart() {Negative = (x != 0), Die = t}));
        }
        return items;
    } 
}
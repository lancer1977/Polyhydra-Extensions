namespace PolyhydraGames.Extensions.Dice;

public struct DiceExpression
{
    public string Value;

    public static DiceExpression Parse(string value)
    {
        return new DiceExpression { Value = value ?? string.Empty };
    }

    public int Roll()
    {
        return DiceRoll.RollDice(Value);
    }

    public int Max()
    {
        return DiceRoll.RollDiceMax(Value);
    }

    public int Min()
    {
        return DiceRoll.RollDiceMin(Value);
    }

    public override string ToString()
    {
        return Value;
    }
}

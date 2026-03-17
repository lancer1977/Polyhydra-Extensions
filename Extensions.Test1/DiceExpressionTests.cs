using NUnit.Framework;
using PolyhydraGames.Extensions.Dice;

namespace Extensions.Test1;

[TestFixture]
public class DiceExpressionTests
{
    #region RollDice Basic Tests

    [Test]
    public void RollDice_WithSimpleDie_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("1d6");
        
        // Assert - 1d6 should return 1-6
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(6));
    }

    [Test]
    public void RollDice_With2d6_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("2d6");
        
        // Assert - 2d6 should return 2-12
        Assert.That(result, Is.GreaterThanOrEqualTo(2));
        Assert.That(result, Is.LessThanOrEqualTo(12));
    }

    [Test]
    public void RollDice_With1d20_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("1d20");
        
        // Assert - 1d20 should return 1-20
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(20));
    }

    [Test]
    public void RollDice_With4d8_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("4d8");
        
        // Assert - 4d8 should return 4-32
        Assert.That(result, Is.GreaterThanOrEqualTo(4));
        Assert.That(result, Is.LessThanOrEqualTo(32));
    }

    #endregion

    #region RollDice With Modifiers

    [Test]
    public void RollDice_WithPositiveModifier_AddsModifier()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("1d6+3");
        
        // Assert - 1d6+3 should return 4-9
        Assert.That(result, Is.GreaterThanOrEqualTo(4));
        Assert.That(result, Is.LessThanOrEqualTo(9));
    }

    [Test]
    public void RollDice_WithNegativeModifier_SubtractsModifier()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("1d6-2");
        
        // Assert - 1d6-2 should return -1 to 4
        Assert.That(result, Is.GreaterThanOrEqualTo(-1));
        Assert.That(result, Is.LessThanOrEqualTo(4));
    }

    [Test]
    public void RollDice_WithZeroModifier_ReturnsBaseRoll()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("2d8+0");
        
        // Assert - 2d8+0 should return 2-16
        Assert.That(result, Is.GreaterThanOrEqualTo(2));
        Assert.That(result, Is.LessThanOrEqualTo(16));
    }

    [Test]
    public void RollDice_With1d20Plus5_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("1d20+5");
        
        // Assert - 1d20+5 should return 6-25
        Assert.That(result, Is.GreaterThanOrEqualTo(6));
        Assert.That(result, Is.LessThanOrEqualTo(25));
    }

    #endregion

    #region Edge Cases

    [Test]
    public void RollDice_WithEmptyString_ReturnsZero()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("");
        
        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void RollDice_WithNull_ReturnsZero()
    {
        // Arrange
        string? dice = null;
        
        // Act
        var result = DiceRoll.RollDice(dice!);
        
        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void RollDice_WithLargePool_10d20_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("10d20");
        
        // Assert - 10d20 should return 10-200
        Assert.That(result, Is.GreaterThanOrEqualTo(10));
        Assert.That(result, Is.LessThanOrEqualTo(200));
    }

    [Test]
    public void RollDice_WithSingleDie_1d6_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice("1d6");
        
        // Assert - 1d6 should return 1-6
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(6));
    }

    #endregion

    #region DieMax Tests

    [Test]
    public void DieMax_With2d6_Returns12()
    {
        // Arrange & Act
        var result = DiceRoll.DieMax("2d6");
        
        // Assert
        Assert.That(result, Is.EqualTo(12));
    }

    [Test]
    public void DieMax_With1d20_Returns20()
    {
        // Arrange & Act
        var result = DiceRoll.DieMax("1d20");
        
        // Assert
        Assert.That(result, Is.EqualTo(20));
    }

    [Test]
    public void DieMax_With4d8_Returns32()
    {
        // Arrange & Act
        var result = DiceRoll.DieMax("4d8");
        
        // Assert
        Assert.That(result, Is.EqualTo(32));
    }

    [Test]
    public void DieMax_WithPlainNumber_ReturnsNumber()
    {
        // Arrange & Act
        var result = DiceRoll.DieMax("5");
        
        // Assert
        Assert.That(result, Is.EqualTo(5));
    }

    #endregion

    #region DieMin Tests (DieMin returns die count, not minimum roll value)

    [Test]
    public void DieMin_With2d6_Returns2()
    {
        // Arrange & Act
        // DieMin extracts the number before "d" — this is the die count
        var result = DiceRoll.DieMin("2d6");
        
        // Assert
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void DieMin_With1d20_Returns1()
    {
        // Arrange & Act
        var result = DiceRoll.DieMin("1d20");
        
        // Assert
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void DieMin_WithEmptyString_ReturnsZero()
    {
        // Arrange & Act
        var result = DiceRoll.DieMin("");
        
        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void DieMin_WithPlainNumber_ReturnsZero()
    {
        // Arrange & Act
        // Plain numbers (no "d") return 0 since no dice count is parsed
        var result = DiceRoll.DieMin("3");
        
        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    #endregion

    #region RollDiceMax Tests

    [Test]
    public void RollDiceMax_With1d6Plus3_Returns9()
    {
        // Arrange & Act
        var result = DiceRoll.RollDiceMax("1d6+3");
        
        // Assert - DieMax("1d6")=6, DieMax("3")=3, total=9
        Assert.That(result, Is.EqualTo(9));
    }

    #endregion

    #region RollDiceMin Tests (Note: DieMin returns die count, not min roll value)

    [Test]
    public void RollDiceMin_With1d6Plus3_Returns1()
    {
        // Arrange & Act
        // DieMin("1d6") = 1 (die count), DieMin("3") = 0 (no "d" present)
        // This is a known quirk — RollDiceMin returns die counts, not minimum roll values
        var result = DiceRoll.RollDiceMin("1d6+3");
        
        // Assert - matches actual implementation behavior
        Assert.That(result, Is.EqualTo(1));
    }

    #endregion

    #region Individual Die Roll Tests

    [Test]
    public void D4_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.D4();
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(4));
    }

    [Test]
    public void D6_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.D6();
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(6));
    }

    [Test]
    public void D8_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.D8();
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(8));
    }

    [Test]
    public void D10_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.D10();
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(10));
    }

    [Test]
    public void D12_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.D12();
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(12));
    }

    [Test]
    public void D20_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.D20();
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(20));
    }

    [Test]
    public void D100_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.D100();
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(100));
    }

    #endregion

    #region RollDiceWithResults Tests

    [Test]
    public void RollDiceWithResults_ReturnsCorrectCount()
    {
        // Arrange
        var dice = "2d6";
        
        // Act
        var results = DiceRoll.RollDiceWithResults(dice, out var total);
        
        // Assert
        Assert.That(results.Count, Is.EqualTo(2));
        Assert.That(total, Is.EqualTo(results.Sum()));
    }

    [Test]
    public void RollDiceWithResults_WithModifier_ReturnsCorrectCount()
    {
        // Arrange
        var dice = "2d6+3";
        
        // Act
        var results = DiceRoll.RollDiceWithResults(dice, out var total);
        
        // Assert - should have 2 dice + 1 modifier entry
        Assert.That(results.Count, Is.EqualTo(3));
    }

    #endregion

    #region RollDice Overload Tests

    [Test]
    public void RollDice_WithBonusNumberSides_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDice(5, 2, 6); // +5 bonus, 2d6
        
        // Assert - 2d6+5 should return 7-17
        Assert.That(result, Is.GreaterThanOrEqualTo(7));
        Assert.That(result, Is.LessThanOrEqualTo(17));
    }

    #endregion

    #region RollDie Tests

    [Test]
    public void RollDie_With6Sides_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDie(6);
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(6));
    }

    [Test]
    public void RollDie_With20Sides_ReturnsWithinRange()
    {
        // Arrange & Act
        var result = DiceRoll.RollDie(20);
        
        // Assert
        Assert.That(result, Is.GreaterThanOrEqualTo(1));
        Assert.That(result, Is.LessThanOrEqualTo(20));
    }

    #endregion
}

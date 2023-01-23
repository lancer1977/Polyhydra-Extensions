using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PolyhydraGames.Extensions.Dice
{
    public static class DiceRoll
    {
        private static readonly Random Rand = new Random();
        /// <summary>
        /// Returns the minimum die roll.
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <returns></returns>
        private static int DieMin(this string dice)
        {
            if (string.IsNullOrEmpty(dice)) return 0;
            dice = dice.ToLower();
            return dice.Contains("d") ? dice.Substring(0, dice.IndexOf("d", StringComparison.Ordinal)).ToInt() : 0;
        }

        /// <summary>
        /// Rolls the dice with results.
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static List<int> RollDiceWithResults(string dice, out int result)
        {
          
            var diceResults = new List<int>();
         
            if (string.IsNullOrEmpty(dice))
            {
                result = 0;
                return diceResults;
            }
            foreach (var item in dice.Split('+'))
                diceResults.AddRange(ParseDiceInts(item));
            result = diceResults.Sum();
            return diceResults;
        }
        /// <summary>
        /// Parses the dice ints.
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <returns></returns>
        private static IEnumerable<int> ParseDiceInts(string dice)
        {

            var dicePool = new List<int>();
            if (string.IsNullOrEmpty(dice))
            {
                return dicePool;
            }
            dice = dice.ToLower();
            if (dice.Contains("d") == false) dicePool.Add(dice.ToInt());
            else
            {
                var split = dice.Split('d');
                if (split.Length < 2) return null;
                var number = split[0].ToInt();
                var type = split[1].ToInt();
                for (var x = 0; x < number; x++)
                    dicePool.Add(D(type).Invoke());
            }
            return dicePool;
        }


        //public static IEnumerable<Func<int>> ParseDice(string dice)
        //{
        //    var dicePool = new List<Func<int>>();
        //    dice = dice.ToLower();
        //    if (dice.Contains("d") == false) dicePool.Add(PlainNumber(dice));
        //    else
        //    {
        //        string[] split = dice.Split('d');
        //        if (split.Length < 2) return null;
        //        int number = split[0].ToInt();
        //        int type = split[1].ToInt();
        //        for (int x = 0; x < number; x++)
        //            dicePool.Add(D(type));
        //    }
        //    return dicePool;
        //}
        private static Func<int> D(int sides)
        {
            return () => RollRandom(1, sides);
        }
         
        /// <summary>
        /// Returns the results of a D4 die roll
        /// </summary>
        /// <returns></returns>
        public static int D4()
        {
            return RollRandom(1, 4);
        }
        /// <summary>
        /// Returns the results of a D6 die roll
        /// </summary>
        /// <returns></returns>
        public static int D6()
        {
            return RollRandom(1, 6);
        }

        public static int D8()
        {
            return RollRandom(1, 8);
        }

        public static int D10()
        {
            return RollRandom(1, 10);
        }

        public static int D12()
        {
            return RollRandom(1, 12);
        }

        public static int D20()
        {
            return RollRandom(1, 20);
        }

        public static int D100()
        {
            return RollRandom(1, 100);
        }

        public static Func<T> ToFunc<T>(T input)
        {
            return () => input;
        }

        /// <summary>
        /// Rolls the random number between two values.
        /// </summary>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <returns></returns>
        public static int RollRandom(int low, int high)
        {
            return Rand.Next(low, high + 1);
        }

        public static Func<T> Partial<TParam1, T>(this Func<TParam1, T> func, TParam1 param)
        {
            return () => func(param);
        }

        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <param name="bonus">The bonus.</param>
        /// <param name="number">The number.</param>
        /// <param name="sides">The sides.</param>
        /// <returns></returns>
        public static int RollDice(int bonus, int number, int sides)
        {
            var total = bonus;
            for (var x = 0; x < number; x++)
                total += RollRandom(1, sides);
            return total;
        }

        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <returns></returns>
        public static int RollDice(string dice)
        {
            if (string.IsNullOrEmpty(dice)) return 0;
            var dicePool = dice.Split('+');
            Debug.WriteLine(dicePool.ToCodedArray());
            return dicePool.Sum(item => RollNDie(item));
        }

        public static int RollDiceMax(string dice)
        {
            if (string.IsNullOrEmpty(dice)) return 0;
            if (dice.Contains("+"))
            {
                var dicePool = dice.Split('+');
                Debug.WriteLine(dicePool.ToCodedArray());
                return dicePool.Sum(item => DieMax(item));
            }
            return DieMax(dice);
        }

        public static int RollDiceMin(string dice)
        {
            if (string.IsNullOrEmpty(dice)) return 0;
            if (dice.Contains("+"))
            {
                var dicePool = dice.Split('+');
                Debug.WriteLine(dicePool.ToCodedArray());
                return dicePool.Sum(item => DieMin(item));
            }
            return DieMin(dice);
        }

        /// <summary>
        /// Returns the maximum result of a die IE. 4d6, 1d8, 12d4, etc. No +'s
        /// </summary>
        /// <param name="dice">The dice.</param>
        /// <returns></returns>
        public static int DieMax(this string dice)
        {
            dice = dice.ToLower();
            if (dice.Contains("d"))
            {
                var split = dice.Split('d');
                var number = split[0].ToInt();
                var sides = split[1].ToInt();
                return number * sides;
            }
            return dice.ToInt();
        }

        /// <summary>
        /// Returns the result of a die IE. 4d6, 1d8, 12d4, etc. No +'s
        /// </summary>
        /// <param name="die">The die.</param>
        /// <returns></returns>
        public static int RollNDie(string die)
        {
            die = die.ToLower();
            if (die.Contains("d"))
            {
                var split = die.Split('d'); 
                var number = split.Count() == 1 ? 1 : split[0].ToInt();
                var sides = split[1].ToInt();
                var total = 0;
                for (var x = 1; x <= number; x++)
                {
                    total = RollDie(sides) + total;
                } 
                Debug.WriteLine("Die:{0} Result:{1}", die, total);
                return total;
            }
            Debug.WriteLine("Die:{0} Result:{1}", die, die);
            return die.ToInt();
        }
        /// <summary>
        /// Rolls a die with N sides
        /// </summary>
        /// <param name="dieSides">The die sides.</param>
        /// <returns></returns>
        public static int RollDie(int dieSides)
        {
            var result = RollRandom(1, dieSides);
            Debug.WriteLine("Die:{0} Result:{1}", "1d" + dieSides, result);
            return result;
        }
         
    }
}
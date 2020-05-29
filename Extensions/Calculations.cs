using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PolyhydraGames.Extensions
{
    public static class Calculations
    {
        public static List<string> EnumTypeToList<T>(bool insertSpace = true)
        {
            var enumValArray = Enum.GetValues(typeof(T));
            var enumList = new List<string>(enumValArray.Length);
            if (insertSpace)
            {
                enumList.AddRange(from object item in enumValArray
                                  select item.ToString().InsSpace());
            }
            else
            {
                enumList.AddRange(from object item in enumValArray
                                  select item.ToString());
            }


            return enumList;
        }

        public static string[] SplitFrontStack(string pItem)
        {
            var aRay = new string[2];
            // var index1 = pItem.IndexOf(" ");
            var strTemp = pItem.Split(' ');

            if (strTemp.Count() > 1)
            {
                aRay[1] = strTemp[1];
                aRay[0] = strTemp[0];
            }

            return aRay;
        }

        public static long TimeEllapsed(this Action act)
        {
            //var sw = new Stopwatch();
            //sw.Start();
            //act();
            //sw.Stop();
            return 1;

        }

        public static int Max(this int value, int max)
        {
            return value <= max ? value : max;
        }

        public static int MinMax(this int value, int min, int max)
        {
            if (value < min)
                value = min;
            return value.Max(max);
        }

        public static int InStr(string target, string find)
        {
            if (target.Contains(find))
            {
                return target.IndexOf(find, StringComparison.Ordinal);
            }
            return -1;
        }

        public static int Buff(this int value, int newValue)
        {
            return newValue > value ? newValue : value;
        }

        public static int Total(this IEnumerable<int> values)
        {
            return values.Sum();
        }

        public static int MinimumValue(this int value, int min)
        {
            return value < min ? min : value;
        }

        public static int GreaterOf(this IEnumerable<int> options)
        {
            return options.Max();
        }

        public static int LesserOf(this IEnumerable<int> options)
        {
            return options.Min();
        }

        //public static int Average(this IEnumerable<int> options)
        //{
        //    var number = options.Count();
        //    return (number > 0) ? options.Sum() / number : 0;
        //}

        public static List<string> RemoveDuplicates(IEnumerable<string> list, IEnumerable<string> discoveries)
        {
            var enumerable = list as IList<string> ?? list.ToList();
            var trimList = enumerable.Where(item => !discoveries.Contains(item)).ToList();
            return trimList;
        }

        public static string Dec2Frac(float f)
        {
            float df = 1f;
            string result = string.Empty;
            int lUpperPart = 1;
            int lLowerPart = 1;

            while (Math.Abs(df - f) > .001)
            {
                if (df < f)
                    lUpperPart = lUpperPart + 1;
                else
                {
                    lLowerPart = lLowerPart + 1;

                    lUpperPart = (int)(f * lLowerPart);
                }
                df = (float)lUpperPart / lLowerPart;
                result = (lUpperPart) + "/" + (lLowerPart);
                Debug.WriteLine(df + " AKA: " + result);

            }
            return (lUpperPart) + "/" + (lLowerPart);

        }

        public static double CompoundInterestAnnual(double principal, double interestRate, double years)
        {
            return CompoundInterest(principal, interestRate, years, 1);
        }

        public static double CompoundInterestMonthly(double principal, double interestRate, double years)
        {
            return CompoundInterest(principal, interestRate, years, 12);
        }
        public static double CompoundInterestDaily(double principal, double interestRate, double years)
        {
            return CompoundInterest(principal, interestRate, years, 365);
        }
        public static double CompoundInterest(double p, double i, double t, int r)
        {
            var rate = i / r;
            var exponential = (t * r);
            return p * Math.Pow(1 + rate, exponential);
        }

        public static double ReverseCompoundInterest(double total, double i, int t, int r)
        {
            var rate = i / r;
            var exponential = (t * r);
            return total / Math.Pow(1 + rate, exponential);
        }

        public static double CompoundInterestContinuously(double p, double i, int t)
        {
            return p * (Math.Pow(Math.E, (i * t)));
        }


    }
}
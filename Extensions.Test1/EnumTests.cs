using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyhydraGames.Extensions;
using PolyhydraGames.Pathfinder.Enumerations;

namespace Extensions.Test1
{
     

    [TestFixture]
    public class EnumTests
    {

        [TestCase("Low-Light Vision", ExpectedResult = ERacialTrait.LowLightVision),
         TestCase("", ExpectedResult = ERacialTrait.Unknown),
         TestCase(null, ExpectedResult = ERacialTrait.Unknown ),
         TestCase("", ExpectedResult = ERacialTrait.Unknown),
         TestCase(null, ExpectedResult = ERacialTrait.Unknown),
        TestCase("Low-light Vision", ExpectedResult = ERacialTrait.LowLightVision)]

        public ERacialTrait ERacialTraitTest(string word )
        {
            var result = word.ToEnum< ERacialTrait>();
            return result;
        }

        [TestCase("Bulls Strength", ExpectedResult = EEffect.BullsStrength)]
        [TestCase("BullsStrength", ExpectedResult = EEffect.BullsStrength)]
        [TestCase("BullsStrent", ExpectedResult = EEffect.Unknown)]
        [TestCase("Shield Other", ExpectedResult = EEffect.ShieldOther)]
        [TestCase("Shield Of Faith", ExpectedResult = EEffect.ShieldofFaith)]
        [TestCase("Bull's Strength", ExpectedResult = EEffect.BullsStrength)]
        [TestCase("BullsStrength", ExpectedResult = EEffect.BullsStrength)]
        [TestCase("BuLlSStrEngth", ExpectedResult = EEffect.BullsStrength)]

        public EEffect EEffectTest(string word)
        {
            var result = word.ToEnum<EEffect>();
            return result;
        }
    }
}

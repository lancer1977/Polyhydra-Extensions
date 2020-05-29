//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PolyhydraGames.Extensions;
//using PolyhydraGames.Pathfinder.Enumerations;

//namespace Extensions.Test1
//{
//    [TestFixture]
//    public class TestClass
//    {
//        [Test]
//        public void TestMethod()
//        {
//            // TODO: Add your test code here
//            Assert.Pass("Your first passing test");
//        }
//    }

//    [TestFixture]
//    public class EnumTests
//    {

//        [TestCase("Low-Light Vision", ERacialTrait.LowLightVision, true),
//         TestCase("", ERacialTrait.LowLightVision, false),
//         TestCase(null, ERacialTrait.LowLightVision, false),
//         TestCase("", ERacialTrait.Unknown, true),
//         TestCase(null, ERacialTrait.Unknown, true),
//        TestCase("Low-light Vision", ERacialTrait.LowLightVision, true)]

//        public void Test1(string word, ERacialTrait effect, bool expected)
//        {
//            var result = word.ToEnum<ERacialTrait>() == effect;
//            if (expected)
//            {
//                Assert.IsTrue(result);
//            }
//            else
//            {
//                Assert.IsFalse(result);
//            }
//        }

//        [TestCase("Bulls Strength", EEffect.BullsStrength, true)]
//        [TestCase("Bulls Strength", EEffect.BullsStrength, true)]
//        [TestCase("BullsStrent", EEffect.Unknown, true)]
//        [TestCase("Shield Other", EEffect.ShieldOther, true)]
//        [TestCase("Shield Of Faith", EEffect.ShieldofFaith, true)]
//        [TestCase("Bull's Strength", EEffect.BullsStrength, true)]
//        [TestCase("BullsStrength", EEffect.BullsStrength, true)]
//        [TestCase("BuLlSStrEngth", EEffect.BullsStrength, true)]

//        public void Test1(string word, EEffect effect, bool expected)
//        {
//            var result = word.ToEnum<EEffect>() == effect;
//            if (expected)
//            {
//                Assert.IsTrue(result);
//            }
//            else
//            {
//                Assert.IsFalse(result);
//            }
//        }
//    }
//}

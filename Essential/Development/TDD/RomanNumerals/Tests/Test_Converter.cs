using NUnit.Framework;
using System;
using static Practice_Converter.Converter;

namespace Tests
{
    [TestFixture]
    public class Test_Converter
    {
        [Test]
        public void TestConverter_OneArabic_OneRoman()
        {
            const string exceptedFormat = "I";

            string actualFormat = ConvertArabicToRoman(1);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_TwoArabic_TwoRomans()
        {
            const string exceptedFormat = "II";

            string actualFormat = ConvertArabicToRoman(2);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_ThreeArabic_ThreeRomans()
        {
            const string exceptedFormat = "III";

            string actualFormat = ConvertArabicToRoman(3);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_FourArabic_FourRomans()
        {
            const string exceptedFormat = "IV";

            string actualFormat = ConvertArabicToRoman(4);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_FiveArabic_FiveRomans()
        {
            const string exceptedFormat = "V";

            string actualFormat = ConvertArabicToRoman(5);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_SixArabic_SixRomans()
        {
            const string exceptedFormat = "VI";

            string actualFormat = ConvertArabicToRoman(6);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_SevenArabic_SevenRomans()
        {
            const string exceptedFormat = "VII";

            string actualFormat = ConvertArabicToRoman(7);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_EightArabic_EightRomans()
        {
            const string exceptedFormat = "VIII";

            string actualFormat = ConvertArabicToRoman(8);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_NineArabic_NineRomans()
        {
            const string exceptedFormat = "IX";

            string actualFormat = ConvertArabicToRoman(9);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_TenArabic_TenRomans()
        {
            const string exceptedFormat = "X";

            string actualFormat = ConvertArabicToRoman(10);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_TwentyArabic_TwentyRomans()
        {
            const string exceptedFormat = "XX";

            string actualFormat = ConvertArabicToRoman(20);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_TwentyOneArabic_TwentyOneRomans()
        {
            const string exceptedFormat = "XXI";

            string actualFormat = ConvertArabicToRoman(21);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_ThirtyArabic_ThirtyRomans()
        {
            const string exceptedFormat = "XXX";

            string actualFormat = ConvertArabicToRoman(30);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_FiveHundredAndTwoArabic_FiveHundredAndTwoRomans()
        {
            const string exceptedFormat = "DII";

            string actualFormat = ConvertArabicToRoman(502);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_OneThousandThreeOneArabic_OneThousandThreeRomans()
        {
            const string exceptedFormat = "MIII";

            string actualFormat = ConvertArabicToRoman(1003);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_OneThousandNineHundredAndNinetyArabic_OneThousandNineHundredAndNinetyRomans()
        {
            const string exceptedFormat = "MCMXC";

            string actualFormat = ConvertArabicToRoman(1990);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }

        [Test]
        public void TestConverter_OneThousandNineHundredAndThreeArabic_OneThousandNineHundredAndThreeRomans()
        {
            const string exceptedFormat = "MCMIII";

            string actualFormat = ConvertArabicToRoman(1903);

            Assert.AreEqual(exceptedFormat, actualFormat);
        }
    }
}

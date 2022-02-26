using ComparisonCompacting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComparisonCompactingTests
{
	[TestClass]
	public class ComparisonCompactorTest
	{
		[TestMethod]
		public void TestMessage()
		{
			string failure = new ComparisonCompactor(0, "b", "c").FormatCompactedComparison("a");
			Assert.IsTrue("a expected:<[b]> but was:<[c]>".Equals(failure));
		}


		[TestMethod]
		public void TestStartSame()
		{
			string failure = new ComparisonCompactor(1, "ba", "bc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<b[a]> but was:<b[c]>", failure);
		}


		[TestMethod]
		public void TestEndSame()
		{
			string failure = new ComparisonCompactor(1, "ab", "cb").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<[a]b> but was:<[c]b>", failure);
		}


		[TestMethod]
		public void TestSame()
		{
			string failure = new ComparisonCompactor(1, "ab", "ab").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<ab> but was:<ab>", failure);
		}


		[TestMethod]
		public void TestNoContextStartAndEndSame()
		{
			string failure = new ComparisonCompactor(0, "abc", "adc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<...[b]...> but was:<...[d]...>", failure);
		}


		[TestMethod]
		public void TestStartAndEndContext()
		{
			string failure = new ComparisonCompactor(1, "abc", "adc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<a[b]c> but was:<a[d]c>", failure);
		}


		[TestMethod]
		public void TestStartAndEndContextWithEllipses()
		{
			string failure = new ComparisonCompactor(1, "abcde", "abfde").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<...b[c]d...> but was:<...b[f]d...>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorStartSameComplete()
		{
			string failure = new ComparisonCompactor(2, "ab", "abc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<ab[]> but was:<ab[c]>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorEndSameComplete()
		{
			string failure = new ComparisonCompactor(0, "bc", "abc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<[]...> but was:<[a]...>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorEndSameCompleteContext()
		{
			string failure = new ComparisonCompactor(2, "bc", "abc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<[]bc> but was:<[a]bc>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorOverlapingMatches()
		{
			string failure = new ComparisonCompactor(0, "abc", "abbc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<...[]...> but was:<...[b]...>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorOverlapingMatchesContext()
		{
			string failure = new ComparisonCompactor(2, "abc", "abbc").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<ab[]c> but was:<ab[b]c>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorOverlapingMatches2()
		{
			string failure = new ComparisonCompactor(0, "abcdde", "abcde").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<...[d]...> but was:<...[]...>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorOverlapingMatches2Context()
		{
			string failure = new ComparisonCompactor(2, "abcdde", "abcde").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<...cd[d]e> but was:<...cd[]e>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorWithActualNull()
		{
			string failure = new ComparisonCompactor(0, "a", null).FormatCompactedComparison(null);
			Assert.AreEqual("expected:<a> but was:<null>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorWithActualNullContext()
		{
			string failure = new ComparisonCompactor(2, "a", null).FormatCompactedComparison(null);
			Assert.AreEqual("expected:<a> but was:<null>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorWithExpectedNull()
		{
			string failure = new ComparisonCompactor(0, null, "a").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<null> but was:<a>", failure);
		}


		[TestMethod]
		public void TestComparisonErrorWithExpectedNullContext()
		{
			string failure = new ComparisonCompactor(2, null, "a").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<null> but was:<a>", failure);
		}


		[TestMethod]
		public void TestBug609972()
		{
			string failure = new ComparisonCompactor(10, "S&P500", "0").FormatCompactedComparison(null);
			Assert.AreEqual("expected:<[S&P50]0> but was:<[]0>", failure);
		}
	}
}

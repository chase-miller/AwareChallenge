using System;
using System.Collections.Generic;
using System.Linq;
using AwareChallenge;
using FluentAssertions;
using NUnit.Framework;

namespace AwareChallengeTests2
{
    public class MissingNumbersExtensionTests
    {
        [TestCaseSource(nameof(TestCases))]
        public void TestCaseDriver((IEnumerable<int> input, string expectedOutput) testCase)
        {
            var (input, expectedOutput) = testCase;
            input.MissingNumbersFromRange(0, 99).Should().Be(expectedOutput);
        }

        [TestCase(0, 0)]
        [TestCase(2, 2)]
        [TestCase(8, 0)]
        [TestCase(8, 7)]
        public void GivenInvalidRanges_ThenThrowException(int bottom, int top)
        {
            Action action = () => new[] {1, 2}.MissingNumbersFromRange(bottom, top);
            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        public static IEnumerable<(IEnumerable<int> input, string expectedOutput)> TestCases()
        {
            yield return
            (
                Enumerable.Empty<int>(),
                "0-99"
            );
            
            yield return 
            (
                new[] { 0 },
                "1-99"
            );

            yield return
            (
                new[] { 3, 5 },
                "0-2,4,6-99"
            );

            yield return
            (
                new[] { 0, 1, 2, 50, 52, 75 },
                "3-49,51,53-74,76-99"
            );
            
            // out of range values should be ignored
            yield return
            (
                new[] { -1, 0, 1, 2, 50, 52, 75, 101 },
                "3-49,51,53-74,76-99"
            );
            
            // out of range values should be ignored 2
            yield return
            (
                new[] { -1, 101, 109 },
                "0-99"
            );
        }
    }
}
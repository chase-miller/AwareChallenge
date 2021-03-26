using AwareChallenge;
using FluentAssertions;
using NUnit.Framework;

namespace AwareChallengeTests2
{
    /*
     * You are given a positive integer number and you have to return a
     * boolean telling whether the input number is a tidy number or not.
     * A tidy number is a number whose digits are in non-decreasing order.
     * For example, 1234 is a tidy number, 122334 is also a tidy number but 143567 is not a tidy number.
     */
    public class TidyNumberExtensionTests
    {
        [TestCase(1234)]
        [TestCase(122334)]
        public void ShouldBeTidyNumber(int number)
        {
            number.IsTidy().Should().BeTrue();
        }

        [TestCase(143567)]
        public void ShouldNotBeTidyNumber(int number)
        {
            number.IsTidy().Should().BeFalse();
        }
    }
}

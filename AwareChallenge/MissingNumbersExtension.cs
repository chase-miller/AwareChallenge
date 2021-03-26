using System;
using System.Collections.Generic;
using System.Linq;

namespace AwareChallenge
{
    public static class MissingNumbersExtension
    {
        public static string MissingNumbersFromRange(this IEnumerable<int> input, int rangeBottom, int rangeTop)
        {
            if (rangeBottom >= rangeTop)
                throw new ArgumentOutOfRangeException(nameof(rangeTop), $"{nameof(rangeTop)} needs to be higher than {nameof(rangeBottom)}");

            var count = (rangeTop - rangeBottom) + 1;

            var missingRanges = Enumerable
                .Range(rangeBottom, count)
                .Except(input)
                .Where(val => val >= rangeBottom && val <= rangeTop)
                .GetRanges();

            return string.Join(",", missingRanges.Select(RangeToString));
        }

        private static IEnumerable<(int bottom, int top)> GetRanges(this IEnumerable<int> valuesInput)
        {
            var values = valuesInput?.ToList() ?? new List<int>();

            if (!values.Any())
                return new List<(int bottom, int top)>();

            var orderedDistinct = values
                .OrderBy(x => x)
                .Distinct();

            // Ok this is a mind-bender...
            return
                // We have to pad the ordered list with the first and last values (otherwise the first and last ranges won't be in the output)
                new[] {values.First()}.Concat(orderedDistinct).Concat(new[] {values.Last()})
                // Then find the sets of numbers that aren't consecutive
                .SelectWithPrevious((previous, current) => (bottom: previous, top: current, isConsecutive: previous + 1 == current))
                .Where(set => !set.isConsecutive)
                // And finally shift the sets. For example, the sets [(0, 0), (2, 4), (4, 6), (99, 99)] should actually be [(0, 2), (4, 4), (6, 99)]
                .SelectWithPrevious((lastSet, currentSet) => (bottom: lastSet.top, top: currentSet.bottom));
        }

        private static string RangeToString((int bottom, int top) range)
        {
            return range.bottom == range.top
                ? $"{range.bottom}" // the "range" is the same, so just output one of the two numbers
                : $"{range.bottom}-{range.top}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwareChallenge
{
    public static class TidyNumberExtensions
    {
        public static bool IsTidy(this int input)
        {
            var ordered = input
                .ToString()
                .ToCharArray()
                .Select(character => int.Parse(character.ToString()))
                .OrderBy(x => x);

            return string.Join("", ordered) == input.ToString();
        }
    }
}

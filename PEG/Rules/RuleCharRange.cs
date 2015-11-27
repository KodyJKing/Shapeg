using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Matches any single character between lower and upper, inclusive. Fails if character is outside range.
    /// </summary>
    class RuleCharRange : Rule
    {
        char lower;
        char upper;

        Boolean inverted;

        public RuleCharRange(char lower, char upper, Boolean inverted = false)
        {
            this.lower = lower;
            this.upper = upper;

            this.inverted = inverted;
        }

        public override bool match(Parser parser)
        {
            char next = parser.next();
            Boolean success = (lower <= next && next <= upper) ^ inverted;
            if (success)
                parser.addResult(next);
            return success;
        }

        public override string expectation()
        {
            return "[" + lower + "-" + upper +"]";
        }
    }
}

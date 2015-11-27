using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Matches one character. Succeeds unless EOF is reached.
    /// </summary>
    class RuleAny : Rule
    {
        public override bool match(Parser parser)
        {
            Char c = parser.next();
            if (c == Char.MinValue)
                return false;
            parser.addResult(c);
            return true;
        }

        public override string expectation()
        {
            return "ANY";
        }
    }
}

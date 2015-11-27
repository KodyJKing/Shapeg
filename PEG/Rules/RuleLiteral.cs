using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Matches a string.
    /// </summary>
    class RuleLiteral : Rule
    {
        String literal;

        public RuleLiteral(String literal)
        {
            this.literal = literal;
        }

        override public Boolean match(Parser parser)
        {
            foreach(char c in literal)
            {
                if (parser.next() != c)
                    return false;
            }
            parser.addResult(literal);
            return true;
        }

        public override string expectation()
        {
            return "\"" + literal + "\"";
        }
    }
}

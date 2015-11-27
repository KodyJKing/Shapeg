using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Matches given rule as many times as possible. Fails if no matches are made.
    /// </summary>
    class RuleOneplus : Rule
    {
        Rule rule;

        public RuleOneplus(Rule rule)
        {
            this.rule = rule;
        }

        override public Boolean match(Parser parser)
        {
            Boolean success = parser.match(rule);
            if (success)
                while (parser.match(rule)) { }
            return success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Matches a given rule until no more matches can be made. Always succeeds.
    /// </summary>
    class RuleAll : Rule
    {
        Rule rule;

        public RuleAll(Rule rule)
        {
            this.rule = rule;
        }

        override public Boolean match(Parser parser)
        {
            while (parser.match(rule)) { }
            return true;
        }
    }
}

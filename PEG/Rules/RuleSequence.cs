using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Matches all given rules in order. Fails if any rule fails to match.
    /// </summary>
    class RuleSequence : Rule
    {
        Rule[] rules;

        public RuleSequence(params Rule[] rules)
        {
            this.rules = rules;
        }

        override public Boolean match(Parser parser)
        {
            foreach(Rule rule in rules)
            {
                if (!parser.match(rule))
                {
                    return false;
                }    
            }
            return true;
        }
    }
}

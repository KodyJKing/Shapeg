using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Try to match each option in order. Fails if none match.
    /// </summary>
    class RuleEither : Rule
    {
        Rule[] rules;

        public RuleEither(params Rule[] rules)
        {
            this.rules = rules;
        }

        override public Boolean match(Parser parser)
        {
            foreach (Rule rule in rules)
            {
                if (parser.match(rule))
                    return true;
            }
            return false;
        }
    }
}

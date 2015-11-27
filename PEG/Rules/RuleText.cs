using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Modifies result of given rule. Results in the text matched by the given rule.
    /// </summary>
    class RuleText : Rule
    {
        public Rule rule;

        public RuleText(Rule rule)
        {
            this.rule = rule;
        }

        public override bool match(Parser parser)
        {
            int start = parser.getIndex();
            parser.openBranch();
            Boolean success = parser.match(rule);
            parser.closeBranch(false);
            if (success)
            {
                String result = parser.text.Substring(start, parser.getIndex() - start);
                parser.addResult(result);
            }
            return success;
        }
    }
}

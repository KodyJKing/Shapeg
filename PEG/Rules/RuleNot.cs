using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Succeeds if given rule fails. Does not advance parser or return results.
    /// </summary>
    class RuleNot : Rule
    {
        Rule rule;

        public RuleNot(Rule rule)
        {
            this.rule = rule;
        }

        public override bool match(Parser parser)
        {
            parser.openBranch(); //Open garbage branch to recieve parse results.
            Boolean success = !parser.match(rule);
            parser.closeBranch(false); //Return no results either way.
            return success;
        }

        public override string expectation()
        {
            String subExpectation = rule.expectation() == null ? rule.GetType().ToString() : rule.expectation();
            return "(NOT " + subExpectation + ")"; 
        }
    }
}

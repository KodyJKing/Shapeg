using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Matches given rule but does not add its results to the abstract syntax tree.
    /// </summary>
    class RuleIgnore : Rule
    {
        Rule rule;

        public RuleIgnore(Rule rule)
        {
            this.rule = rule;
        }

        public override bool match(Parser parser)
        {
            parser.openBranch();
            Boolean success = parser.match(rule);
            parser.closeBranch(false);
            return success;
        }
    }
}

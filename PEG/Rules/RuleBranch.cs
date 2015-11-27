using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Opens a new branch in the abstract syntax tree. The results of the given rule are added to the new branch. Succeeds if given rule succeeds.
    /// </summary>
    class RuleBranch : Rule
    {
        Rule rule;

        public RuleBranch(Rule rule)
        {
            this.rule = rule;
        }

        public override bool match(Parser parser)
        {
            parser.openBranch();
            Boolean success = parser.match(rule);
            parser.closeBranch(success);
            return success;
        }
    }
}

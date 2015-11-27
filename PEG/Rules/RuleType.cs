using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Sets the type of the current abstract syntax tree branch.
    /// </summary>
    class RuleType : Rule
    {
        String type;
        Rule rule;

        public RuleType(String type, Rule rule)
        {
            this.type = type;
            this.rule = rule;
        }

        public override bool match(Parser parser)
        {
            parser.openBranch();
            parser.currentBranch().type = type;
            Boolean success = parser.match(rule);
            parser.closeBranch(success);
            return success;
        }
    }
}

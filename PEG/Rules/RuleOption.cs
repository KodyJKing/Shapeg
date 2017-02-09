using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    class RuleOption : Rule
    {
        Rule rule;

        public RuleOption(Rule rule)
        {
            this.rule = rule;
        }

        public override bool match(Parser parser)
        {
            parser.match(rule);
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG.Rules
{
    /// <summary>
    /// Looks up a rule in the parser's given grammar. Necessary for recursive/circular defenitions.
    /// </summary>
    class RuleReference : Rule
    {
        String name;

        public RuleReference(String name)
        {
            this.name = name;
        }

        public override bool match(Parser parser)
        {
            Boolean success = parser.match(parser.grammar[name]);
            return success;
        }

        public override string expectation()
        {
            return "["+name+"]";
        }
    }
}

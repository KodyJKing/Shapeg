using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG
{
    public class Debugger
    {
        private TextLocation furthestLoc;
        private HashSet<Rule> issues = new HashSet<Rule>();

        public void addIssue(TextLocation loc, Rule rule)
        {
            if (loc.index > furthestLoc.index) //Only recall issues encountered at the furthest point in the text.
            {
                issues.Clear();
                furthestLoc = loc;
            }
            if (loc.index >= furthestLoc.index)
                issues.Add(rule);
        }

        public String message()
        {
            StringBuilder result = new StringBuilder("Expected at ");
            result.Append(furthestLoc);
            result.Append(":\n");
            int i = 0;
            foreach(Rule rule in issues)
            {
                String expectation = rule.expectation();
                if (expectation == null)
                    continue;
                if (i > 0)
                    result.Append(", ");
                result.Append(expectation);
                i++;
            }
            return result.ToString();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEG.Rules;

namespace PEG
{
    public abstract class Rule
    {
        abstract public Boolean match(Parser parser);

        virtual public String expectation() { return null; }

        public static Rule operator |(Rule a, Rule b)
        {
            return new RuleEither(a, b);
        }

        public static Rule operator &(Rule a, Rule b)
        {
            return new RuleSequence(a, b);
        }

        public static Rule operator !(Rule a)
        {
            return new RuleNot(a);
        }

        public static Rule operator -(Rule a)
        {
            return new RuleIgnore(a);
        }

        public static Rule operator ~(Rule a)
        {
            return new RuleText(a);
        }
    }
}

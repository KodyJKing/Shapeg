using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEG.Rules;

namespace PEG
{
    public class Shorthand
    {

        public static Rule l(String literal)
        {
            return new RuleLiteral(literal);
        }

        public static Rule and(params Rule[] rules)
        {
            return new RuleSequence(rules);
        }

        public static Rule or(params Rule[] rules)
        {
            return new RuleEither(rules);
        }

        public static Rule all(Rule rule)
        {
            return new RuleAll(rule);
        }

        public static Rule r(String name)
        {
            return new RuleReference(name);
        }

        public static Rule one(Rule rule)
        {
            return new RuleOneplus(rule);
        }

        public static Rule opt(Rule rule)
        {
            return new RuleOption(rule);
        }

        public static Rule chars(char lower, char upper, Boolean inverted = false)
        {
            return new RuleCharRange(lower, upper, inverted);
        }

        public static Rule not(Rule rule)
        {
            return new RuleNot(rule);
        }

        public static Rule ig(Rule rule)
        {
            return new RuleIgnore(rule);
        }

        public static Rule br(Rule rule)
        {
            return new RuleBranch(rule);
        }

        public static Rule type(String type, Rule rule)
        {
            return new RuleType(type, rule);
        }

        public static Rule text(Rule rule)
        {
            return new RuleText(rule);
        }

        public static Rule ANY = new RuleAny();

        public static Rule EOF = l("\0"); //End of file/input

        public static Rule ALPHA = or(chars('a', 'z'), chars('A', 'Z'));

        public static Rule NUM = chars('0', '9');

        public static Rule WS = or(l(" "), l("\t"), l("\n"), l("\r\n"), l("\r"));
    }
}

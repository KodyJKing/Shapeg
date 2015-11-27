using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG
{
    public class Grammar : Shorthand
    {
        Dictionary<String, Rule> rules;

        public Grammar()
        {
            rules = new Dictionary<string, Rule>();
        }

        public Rule this[String name]
        {
            get { return rules[name]; }
            set
            {
                Rule result;
                if (name[0] == '*')
                {
                    name = name.Substring(1, name.Length - 1);
                    result = type(name, value);
                }
                else
                    result = value;
                    
                rules[name] = result;
            }
        }
    }
}

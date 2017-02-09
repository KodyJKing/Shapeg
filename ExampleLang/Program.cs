using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEG;

namespace ExampleLang
{
    class Program : Shorthand
    {
        static void Main(string[] args)
        {
            AST program = ExampleParser.parse(Properties.Resources.ExampleSource);
            ExampleInterpreter.run(program, new Dictionary<string, object>());
        }
    }
}
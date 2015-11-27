using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEG;

namespace DisCLang
{
    class Program : Shorthand
    {
        static void Main(string[] args)
        {
            DisCParser.parse(Properties.Resources.ExampleSource);
        }
    }
}
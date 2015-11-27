using PEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisCLang
{
    class DisCParser : Shorthand
    {
        public static void print(Object o)
        {
            Console.WriteLine(o);
        }

        public static void parse(String text)
        {
            Grammar g = new Grammar();
            Rule equals = -l("=");
            Rule quote = -l("\"");
            Rule dot = -l(".");
            Rule ws = all(WS);

            g["*identifier"] = ~one(ALPHA | NUM);
            g["*string"] = quote & ~all(!quote & ANY) & quote;
            g["*integer"] = ~one(NUM);
            g["*float"] = ~(one(ALPHA) & dot & one(ALPHA));

            g["*assignment"] = r("identifier") & equals & r("expression");

            g["add?"] = r("add") | r("mul?");
            g["*add"] = r("mul?") & ws & -l("+") & ws & r("add?");
            g["mul?"] = r("mul") | r("expression");
            g["*mul"] = r("expression") & ws & -l("*") & ws & r("mul?");

            g["expression"] = r("integer") | r("float") | r("string");

            Parser p = new Parser(text, g);
            print(text);
            print(p.parse(r("add?") & EOF));
            print(p.debugger.message());
        }
    }
}

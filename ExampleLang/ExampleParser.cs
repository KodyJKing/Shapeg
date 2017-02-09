using PEG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleLang
{
    class ExampleParser : Shorthand
    {
        public static void print(Object o)
        {
            Console.WriteLine(o);
        }

        public static Rule s(String str) //Structural symbol.
        {
            return -(all(WS) & l(str) & all(WS));
        }

        public static AST parse(String text)
        {
            Grammar g = new Grammar();

            Rule ws = -all(WS);
            Rule quote = -l("\"");
            Rule dot = -l(".");
            Rule comma = s(",");
            Rule lpar = s("(");
            Rule rpar = s(")");
            Rule equals = s("=");
            Rule add = s("+");
            Rule mul = s("*");
            Rule semi = s(";");

            g["*identifier"] = ~one(ALPHA | NUM);
            g["*string"] = quote & ~all(!quote & ANY) & quote;
            g["*integer"] = ~one(NUM);
            g["*float"] = ~(one(ALPHA) & dot & one(ALPHA));

            g["*assignment"] = r("identifier") & equals & r("expression");
            g["statement"] = ws & (r("assignment") | r("expression")) & semi;
            g["*program"] = all(r("statement")) & EOF;

            g["expression"] = r("add") | r("mul?");
            g["*add"] = r("mul?") & add & r("expression");
            g["mul?"] = r("mul") | r("term");
            g["*mul"] = r("term") & mul & r("mul?");

            g["term"] = r("call") | r("parenthesis") | r("string") | r("integer") | r("float") | r("identifier");
            g["parenthesis"] = lpar & r("expression") & rpar;
            g["*call"] = r("identifier") & lpar & opt(r("arguments")) & rpar;
            g["arguments"] = r("expression") & all(comma & r("expression"));

            Parser p = new Parser(text, g);
            print(text);
            print(p.parse(r("program")));
            print(p.debugger.message());
            print(p.getResult());

            return p.getResult()[0];
        }
    }
}

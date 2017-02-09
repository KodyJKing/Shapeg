using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PEG
{
    class Examples : Shorthand
    {
        public void print(Object o)
        {
            Console.WriteLine(o);
        }

        public void run()
        {
            //Important note: Some of the functions used here are defined in the Shorthand class. Without them, specifying a grammar would be very verbose.
            //It is reccommended to extend the Shorthand class to get access to those functions.

            //basicParsing();
            //parseAndOr();
            //parsingLotsOfThings();
            //usingGrammars();
            //gettingResults();
            //doingMath();
            //parseGrammar();

            Console.ReadKey();
        }

        public void basicParsing()
        {
            Parser parser = new Parser("Hello Parsing!"); //Something to parse!
            Rule exampleRule = l("Hello Parsing!") & EOF; //Matches literal: "Hello Parsing!"
            print(parser.parse(exampleRule)); //Will determine if the rule matches the given text.
            print(parser.debugger.message());
        }

        public void parseAndOr()
        {
            Parser parser1 = new Parser("Hello World!");
            Parser parser2 = new Parser("Hello Parsing!");

            Rule or = l("Parsing") | l("World"); //Will match "Parsing" or "World"
            Rule and = l("Hello ") & or & l("!"); //Will match "Hello " then either then "!"

            print(parser1.parse(and)); //Both should succeed.
            print(parser1.debugger.message());
            //print(parser2.parse(and));
        }

        public void parsingLotsOfThings()
        {
            //Here we will parse many letters to parse words, and many words to parse a sentence.

            Parser p0 = new Parser("This is a sentence."); //Should succeed
            Parser p1 = new Parser("This is a bigger sentence."); //Should succeed
            Parser p2 = new Parser("This lacks punctuation and shouldnt parse"); //Should fail

            Rule period = l(".");
            Rule word = one(ALPHA); //One means match atleast one of the given rule and keep matching until no more can be matched. ALPHA matches any letter.
            Rule optionalWhiteSpace = all(WS); //All matches its given rule as many times as possible. It will succeed even if no matches are made. WS matches whitespace.
            Rule sententce = one(word & optionalWhiteSpace) & period; //The sentence should have atleast one word and end with a period.

            print(p0.parse(sententce));
            print(p1.parse(sententce));
            print(p2.parse(sententce));
        }

        public void usingGrammars()
        {
            //Here we'll use grammar objects to parse nested lists. Recursive defenitions require the grammar object.

            Grammar g = new Grammar();

            Rule leftBracket = l("[");
            Rule rightBracket = l("]");
            Rule comma = l(",");

            g["list"] = leftBracket & r("object") & all(comma & r("object")) & rightBracket; // r(ruleName) is used to reference another rule in the grammar.
            g["object"] = r("terminal") | r("list"); //Note that list and object are defined recursively.
            g["terminal"] = l("0");

            Parser p0 = new Parser("[0,0,0]", g); //The parser's constructor has an optional parameter for a grammar.
            Parser p1 = new Parser("[0,0,[0,0,[0]]]", g);

            print(p0.parse(r("list")));
            print(p1.parse(r("list")));
        }

        public void gettingResults()
        {
            //Here well parse a simple list of numbers but for the first time we'll look at the results, not just whether or not it matched.

            Rule number = ~one(NUM); // ~rule returns the TEXT matched by rule instead of returning the rule's normal result. NUM matches a single numeric character.
            Rule comma = -l(","); // We don't need to see the commas, -rule matches the rule but returns no values.
            Rule numberList = number & all(comma & number);

            Parser p0 = new Parser("1,2,3,4,5");
            Parser p1 = new Parser("4,2");

            print(p0.parse(numberList));
            print(p0.getResult()); //Parser.getResult returns the AST created during parsing. The AST has a nice pretty print functionality for debugging.

            print(p1.parse(numberList));
            print(p1.getResult());
        }

        public void doingMath()
        {
            //Here we will parse a more complicated expression and do something with the results. We will parse mathematical expressions on integers with operations +, *, ^.

            Grammar g = new Grammar();
            Rule lpar = -l("(");
            Rule rpar = -l(")");
            Rule mult = -l("*");
            Rule add = -l("+");
            Rule expo = -l("^");

            g["addative"] = r("add") | r("multaplicative");
            g["*add"] = r("multaplicative") & add & r("addative");

            g["multaplicative"] = r("mult") | r("exponential");
            g["*mult"] = r("exponential") & mult & r("multaplicative");

            g["exponential"] = r("expo") | r("primary");
            g["*expo"] = r("primary") & expo & r("exponential");

            g["primary"] = r("integer") | r("parenthesis");
            g["*integer"] = ~one(NUM); //Preceeding a defenition with a * tells the parser to parse the rule into a new branch and set the branch's 'type' to the rule name.
            g["parenthesis"] = lpar & r("addative") & rpar;

            String expression = "(2+1)*2^3+1";
            print("Parsing:\n" + expression + "\n");
            Parser p = new Parser(expression, g);
            print(p.parse(r("addative") & -EOF) ? "Matched!" : "Failed!");
            //print(p.debugger.message());
            AST result = p.getResult()[0];
            print("\nResulting AST:");
            print(result);
            print("Result:");
            print(convertMath(result));
        }

        public int convertMath(AST mathExp)
        {
            if (mathExp.type == "integer")
                return Int32.Parse(mathExp[0].text);
            int arg0, arg1;
            arg0 = convertMath(mathExp[0]);
            arg1 = convertMath(mathExp[1]);
            switch (mathExp.type)
            {
                case "add":
                    return arg0 + arg1;
                case "mult":
                    return arg0 * arg1;
                case "expo":
                    return (int)Math.Pow(arg0, arg1);
            }
            throw new NotImplementedException();
        }

        public void parseGrammar()
        {
            Grammar g = new Grammar();
            Rule quote = -l("\"");
            Rule semicolon = -l(";");
            Rule ws = -all(WS);
            Rule nws = -one(WS);
            Rule or = ws & -l("|") & ws;
            Rule equals = ws & -l("=") & ws;
            Rule lpar = ws & -l("(") & ws;
            Rule rpar = ws & -l(")") & ws;
            Rule star = -l("*");
            Rule plus = -l("+");
            Rule lbrack = -l("[");
            Rule rbrack = -l("]");
            Rule dash = -l("-");

            g["*identifier"] = ~one(ALPHA | NUM);
            g["*string"] = quote & ~all(!quote & ANY) & quote;
            g["*charrange"] = lbrack & ANY & dash & ANY & rbrack;
            g["*assignment"] = ws & r("identifier") & equals & r("expression") & ws;
            g["*grammar"] = one(r("assignment") & semicolon & ws);
            g["expression"] = r("seq");

            g["seq"] = r("sequence") | r("opt");
            g["*sequence"] = r("opt") & one(nws & r("opt"));

            g["opt"] = r("options") | r("primative");
            g["*options"] = r("multi") & one(or & r("multi"));

            g["multi"] = r("multiple") | r("multiple+") | r("primative");
            g["*multiple"] = r("primative") & star;
            g["*multiple+"] = r("primative") & plus;

            g["primative"] = r("identifier") | r("string") | r("charrange") | r("parenthesis");
            g["parenthesis"] = lpar & r("seq") & rpar;

            Parser p = new Parser(Properties.Resources.Grammar, g); //Source is Resourses.Grammar
            Boolean success = p.parse(r("grammar") & -EOF);
            print(success ? "Successfully parsed!" : "Failed to parse!");
            if(!success)
                print(p.debugger.message());
            print(p.getResult());
        }
    }
}

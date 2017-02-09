using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PEG;

namespace ExampleLang
{
    class ExampleInterpreter
    {
        public static Object run(AST ast, Dictionary<String, Object> environment)
        {
            Console.WriteLine(ast.type);
            switch (ast.type)
            {
                case "program":
                    foreach(AST statement in ast.branches)
                    {
                        run(statement, environment);
                    }
                    break;
                case "assignment":
                    String identifer = idenName(ast[0]);
                    environment[identifer] = run(ast[1], environment);
                    break;
                case "identifier":
                    return environment[idenName(ast)];
                case "add":
                case "mul":
                    int left = (int)run(ast[0], environment);
                    int right = (int)run(ast[1], environment);
                    return binary(ast.type, left, right);
                case "integer":
                    return Int32.Parse(ast[0].text);
                case "call":
                    String funcName = idenName(ast[0]);
                    if(funcName == "print")
                    {
                        Console.WriteLine(run(ast[1], environment));
                    }
                    break;
                default:
                    return null;
            }
            return null;
        }

        public static String idenName(AST iden)
        {
            return iden[0].text;
        }

        public static Object binary(String type, Object left, Object right)
        {
            switch (type)
            {
                case "add":
                    return (int)left + (int)right;
                case "mul":
                    return (int)left * (int)right;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG
{
    public class Parser
    {
        public Grammar grammar;
        public String text;

        private Stack<AST> results;

        private Stack<TextLocation> previousLocations;
        private TextLocation location;

        public Debugger debugger;

        public Parser(String text, Grammar grammar = null)
        {
            this.grammar = grammar;
            this.text = text;
        }

        /// <summary>
        /// Parse given rule return whether or not it matches. Call Parser.getResult() after calling this to get the resulting AST.
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public Boolean parse(Rule rule)
        {
            results = new Stack<AST>();
            previousLocations = new Stack<TextLocation>();
            location = new TextLocation();
            debugger = new Debugger();
            openBranch();
            return match(rule);
        }

        public Boolean match(Rule rule)
        {
            Boolean success;
            start();
            success = rule.match(this);
            end(success);
            if (!success)
                debugger.addIssue(location, rule);
            return success;
        }

        public void start()
        {
            openBranch(); //Add parsed values to new branch so they can be discarded if the match fails.
            previousLocations.Push(location);
        }

        public void end(Boolean success)
        {
            mergeBranch(success); //If the match passed, merge the parsed values into its parent's branch. Otherwise discrard the branch.
            TextLocation previousLoc = previousLocations.Pop();
            if (!success)
            {
                location = previousLoc;
            }
                
        }

        public int getIndex()
        {
            return location.index;
        }

        public char next()
        {
            if (location.index >= text.Length)
                return Char.MinValue;
            location.column++;
            char character = text[location.index++];
            if (isNewline(character))
            {
                location.column = 0;
                location.line++;
            }
            return character;
        } 

        public Boolean isNewline(char c)
        {
            switch (c)
            {
                case '\n':
                case '\r':
                    return true;
                default:
                    return false;
            }
        }

        public void addResult(AST value)
        {
            currentBranch().add(value);
        }

        public void addResult(String value)
        {
            addResult(new AST(value));
        }

        public void addResult(Char value)
        {
            addResult(new AST(value.ToString()));
        }

        public AST currentBranch()
        {
            return results.Peek();
        }

        /// <summary>
        /// The resulting AST of parsing a rule. Call after calling Parser.parse.
        /// </summary>
        /// <returns></returns>
        public AST getResult()
        {
            return currentBranch();
        }

        public void openBranch()
        {
            results.Push(new AST());
        }

        public void closeBranch(Boolean keep)
        {
            AST branch = results.Pop(); //Remove either way.
            if (keep)
                addResult(branch);
        }

        public void mergeBranch(Boolean success)
        {
            AST branch = results.Pop(); //Remove either way.
            if (success)
                currentBranch().mergeWith(branch);
        }
    }
}

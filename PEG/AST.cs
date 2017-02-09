using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG
{
    public class AST
    {
        public String text;
        public String type;

        public List<AST> branches;

        public AST this[int i]
        {
            get { return branches[i]; }
        }

        public AST()
        {
            branches = new List<AST>();
        }

        public AST(params AST[] branches)
        {
            this.branches = new List<AST>(branches);
        }

        public AST(String text)
        {
            this.text = text;
        }

        public Boolean isLeaf()
        {
            return branches == null;
        }

        public void add(AST branch)
        {
            branches.Add(branch);
        }

        public void mergeWith(AST other)
        {
            if(other.type != null)
                type = other.type;
            branches.AddRange(other.branches);
        }

        public void buildPrettyString(StringBuilder s, int depth)
        {
            s.Append('\n');
            s.Append(' ', depth);
            if (isLeaf())
            {
                s.Append(text);
                return;
            }
            if (type != null)
            {
                s.Append(type);
                s.Append(":");
            }
            
            foreach (AST branch in branches)
            {
                branch.buildPrettyString(s, depth+3);
            }
        }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();
            buildPrettyString(result, 0);
            return result.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEG
{
    public struct TextLocation
    {
        public int line, column, index;

        public override string ToString()
        {
            return String.Format("(Char {2}, Column {1}, Line {0})", line, column, index);
        }
    }
}

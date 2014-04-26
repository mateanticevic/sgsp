using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.Script
{
    class CodeChunk
    {
        public CodeChunk(int priority, string code)
        {
            Priority = priority;
            Code = code;
        }
        public int Priority { get; set; }
        public string Code { get; set; }
    }
}

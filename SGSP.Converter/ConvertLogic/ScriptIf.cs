using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.CodeResources.Snippets
{
    class ScriptIf
    {
        private string action;
        private string condition;

        public ScriptIf(string action, string condition)
        {
            this.action = action;
            this.condition = condition;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{action}", action);
            rpl.Add("{condition}", condition);

            return Generator.Snippet(Resources.Snippet.If, rpl);
        }
    }
}

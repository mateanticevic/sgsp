using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptVisibility
    {
        private string condition;

        public ScriptVisibility(string c)
        {
            condition = c;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{condition}", condition);

            return Generator.Snippet(Resources.Snippet.Visibility, rpl);
        }
    }
}

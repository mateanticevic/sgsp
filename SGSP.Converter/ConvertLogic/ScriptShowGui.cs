using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptShowGui
    {
        private string id;
        private string action;

        public ScriptShowGui(string objectId, string actions)
        {
            id = objectId;
            action = actions;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{id}", id);
            rpl.Add("{action}", action);

            return Generator.Snippet(Resources.Snippet.ShowGui, rpl);
        }
        
    }
}

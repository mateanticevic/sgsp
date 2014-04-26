using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptActivateGui
    {
        private string id;

        public ScriptActivateGui(string objectId)
        {
            id = objectId;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{id}", id);

            return Generator.Snippet(Resources.Snippet.ActivateGui, rpl);
        }
        
    }

}

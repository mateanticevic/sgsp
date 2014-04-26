using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptBackground
    {

        private string uri;
        private string id;

        public ScriptBackground(string bgId, string bgUri)
        {
            id = bgId;
            uri = bgUri;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{uri}", uri);
            rpl.Add("{id}", id);

            return Generator.Snippet(Resources.Snippet.Background, rpl);
        }
        
    }
}

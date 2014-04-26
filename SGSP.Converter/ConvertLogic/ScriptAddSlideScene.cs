using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptAddSlideScene
    {
        private int slideNumber;
        private string uri;
        private string id;

        public ScriptAddSlideScene(string idd, int sn, string ur)
        {
            slideNumber = sn;
            uri = ur;
            id = idd;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{id}", id);
            rpl.Add("{uri}", uri);
            rpl.Add("{slide}", slideNumber.ToString());

            return Generator.Snippet(Resources.Snippet.AddSlideScene, rpl);
        }
    }
}

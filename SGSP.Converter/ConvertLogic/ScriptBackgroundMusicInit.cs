using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptBackgroundMusicInit
    {
        private string uri;

        public ScriptBackgroundMusicInit(string x)
        {
            uri = x;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{uri}", uri);

            return Generator.Snippet(Resources.Snippet.BackgroundMusicInit, rpl);
        }
    }
}

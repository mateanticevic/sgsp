using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptBackgroundMusic
    {
        private string _uri;

        public ScriptBackgroundMusic(string uri)
        {
            _uri = uri;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{uri}", _uri);

            return Generator.Snippet(Resources.Snippet.BackgroundMusic, rpl);
        }
        public static void GenerateActivity()
        {

        }
    }
}

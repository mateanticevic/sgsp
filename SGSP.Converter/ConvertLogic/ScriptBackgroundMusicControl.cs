using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptBackgroundMusicControl
    {
        private string condition;

        public ScriptBackgroundMusicControl(string c)
        {
            condition = c;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{condition}", condition);

            return Generator.Snippet(Resources.Snippet.BackgroundMusicControl, rpl);
        }
    }
}

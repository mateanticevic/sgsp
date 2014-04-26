using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptNextSlideScene
    {
        private string id;

        public ScriptNextSlideScene(string idd)
        {
            id = idd;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{id}", id);

            return Generator.Snippet(Resources.Snippet.NextSlideScene, rpl);
        }
    }
}

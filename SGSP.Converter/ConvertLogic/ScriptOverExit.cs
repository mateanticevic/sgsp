using SGSP.eAdventure.SceneItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptOverExit
    {
        private string target;
        private string description;

        public ScriptOverExit(string description, string exitId)
        {
            target = exitId;
            this.description = description;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{exit}", target);
            rpl.Add("{description}", description);


            return Generator.Snippet(Resources.Snippet.OverExit, rpl);
        }
    }
}

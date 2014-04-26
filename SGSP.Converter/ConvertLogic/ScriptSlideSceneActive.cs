using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptSlideSceneActive
    {
        private string ss;
        private string ms;

        public ScriptSlideSceneActive(string slideScene, string mainScript)
        {
            ss = slideScene;
            ms = mainScript;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{ss}", ss);
            rpl.Add("{mainScript}", ms);

            return Generator.Snippet(Resources.Snippet.SlideSceneActive, rpl);
        }
        
    }
}

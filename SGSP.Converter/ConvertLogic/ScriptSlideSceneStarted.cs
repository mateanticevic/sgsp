using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptSlideSceneStarted
    {
        private string ss;
        private string ms;

        public ScriptSlideSceneStarted(string slideScene, string mainScript)
        {
            ss = slideScene;
            ms = mainScript;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{ss}", ss);
            rpl.Add("{mainScript}", ms);

            return Generator.Snippet(Resources.Snippet.SlideSceneStarted, rpl);
        }
    }
}

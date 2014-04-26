using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptCameraDefault
    {


        public ScriptCameraDefault()
        {

        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();


            return Generator.Snippet(Resources.Snippet.CameraDefault, rpl);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptOverObject
    {
        private string target;
        private string description;

        public ScriptOverObject(string description, string objectId)
        {
            target = objectId;
            this.description = description;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{object}", target);
            rpl.Add("{description}", description);


            return Generator.Snippet(Resources.Snippet.OverObject, rpl);
        }
    }
}

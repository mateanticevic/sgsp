using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptNotifyDependingObjects
    {
        private string go;
        private string field;
        private string script;
        private string value;

        public ScriptNotifyDependingObjects(string gameObject, string variable, string scriptName, string val)
        {
            go = gameObject;
            field = variable;
            script = scriptName;
            value = val;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{gameObject}", go);
            rpl.Add("{value}", value);
            rpl.Add("{field}", field);
            rpl.Add("{scriptName}", script);

            return Generator.Snippet(Resources.Snippet.NotifyDependingObjects, rpl);
        }
    }
}

using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptIsOver
    {
        private string objectId;
        private Transform transform;

        public ScriptIsOver(string objectId, Transform transform)
        {
            this.objectId = objectId;
            this.transform = transform;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{overObject}", objectId);

            rpl.Add("{x}", transform.X.ToString());
            rpl.Add("{y}", transform.Y.ToString());
            rpl.Add("{width}", transform.Width.ToString());
            rpl.Add("{height}", transform.Height.ToString());

            return Generator.Snippet(Resources.Snippet.IsOver, rpl);
        }
    }
}

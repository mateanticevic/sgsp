using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptActivateGuiConditioned
    {
        private string id;
        private Transform transform;

        public ScriptActivateGuiConditioned(string objectId, Transform objectTransform)
        {
            id = objectId;
            transform = objectTransform;
        }


        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{id}", id);

            rpl.Add("{x}", transform.X.ToString());
            rpl.Add("{y}", transform.Y.ToString());
            rpl.Add("{width}", transform.Width.ToString());
            rpl.Add("{height}", transform.Height.ToString());

            return Generator.Snippet(Resources.Snippet.ActivateGuiConditioned, rpl);
        }
    }
}

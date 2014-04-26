using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptObjectImage
    {
        private string id;
        private string uri;

        public ScriptObjectImage(string objectId, string imageUri)
        {
            id = objectId;
            uri = imageUri;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{id}", id);
            rpl.Add("{uri}", uri);

            return Generator.Snippet(Resources.Snippet.ObjectImage, rpl);
        }
    }
}

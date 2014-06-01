using SGSP.eAdventure.Common;
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

        private float x;
        private float y;
        private int height;
        private int width;

        public ScriptObjectImage(string objectId, string imageUri)
        {
            id = objectId;
            uri = imageUri;

            x = 0;
            y = 0;
            height = 600;
            width = 800;
        }

        public ScriptObjectImage(string objectId, string imageUri, Transform t)
        {
            id = objectId;
            uri = imageUri;

            x = (float)(t.X / 116.775);
            y = t.Y;
            height = t.Height;
            width = t.Width;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{id}", id);
            rpl.Add("{uri}", uri);

            rpl.Add("{x}", x.ToString().Replace(',','.') + "f");
            rpl.Add("{y}", y.ToString().Replace(',', '.') + "f");
            rpl.Add("{height}", height.ToString());
            rpl.Add("{width}", width.ToString());

            return Generator.Snippet(Resources.Snippet.ObjectImage, rpl);
        }
    }
}

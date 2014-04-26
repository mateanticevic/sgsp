using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.SceneItems.Resources
{
    public class Asset
    {
        public string Type { get; set; }
        public string Uri { get; set; }

        public string UriUnityFormat
        {
            get
            {
                string extension = Path.GetExtension(Uri);
                return Uri.Replace(extension, String.Empty);
            }
        }
    }
}

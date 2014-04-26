using SGSP.eAdventure.SceneItems.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.Common
{
    public class ResourceList
    {
        public ResourceList()
        {
            Assets = new List<Asset>();
        }

        public List<Asset> Assets { get; set; }
        public Condition Condition { get; set; }


    }
}

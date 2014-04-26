using SGSP.eAdventure.Common;
using SGSP.eAdventure.SceneItems.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure
{
    public class Character : BaseElement
    {
        public Character()
        {
            Assets= new List<Asset>();
        }
        public List<Asset> Assets { get; set; }
        public Description Description { get; set; }
        public TextColor TextColor { get; set; }
    }
}

using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.SceneItems
{
    public class ActiveArea : BaseElement
    {
        public Description Description { get; set; }
        public Transform Transform { get; set; }
        public Use Use { get; set; }
    }
}

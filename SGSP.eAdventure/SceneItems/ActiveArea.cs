using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = SGSP.eAdventure.Common.Action;

namespace SGSP.eAdventure.SceneItems
{
    public class ActiveArea : BaseElement
    {
        public ActiveArea()
        {
            Actions = new List<Action>();
        }

        public List<Action> Actions { get; set; }
        public Description Description { get; set; }
        public Transform Transform { get; set; }
        public Use Use { get; set; }
    }
}

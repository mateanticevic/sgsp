using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Action = SGSP.eAdventure.Common.Action;

namespace SGSP.eAdventure
{
    public class Object : BaseElement
    {
        public Object()
        {
            Actions = new List<Action>();
            Description = new Description();
            Resources = new List<ResourceList>();
        }
        public List<Action> Actions { get; set; }
        public Description Description { get; set; }
        public List<ResourceList> Resources { get; set; }
        public Use Use { get; set; }
        public string PropertyOver
        {
            get
            {
                return "Over" + this.Id;
            }
        }
    }
}

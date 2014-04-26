using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure
{
    public class Object : BaseElement
    {
        public Object()
        {
            Description = new Description();
            Resources = new List<ResourceList>();
        }
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

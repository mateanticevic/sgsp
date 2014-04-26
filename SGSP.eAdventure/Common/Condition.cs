using SGSP.eAdventure.Common.ConditionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.Common
{
    public class Condition
    {
        public Condition()
        {
            Actives = new List<Active>();
            Inactives = new List<Inactive>();
        }

        public List<Active> Actives { get; set; }
        public List<Inactive> Inactives { get; set; }

        public bool IsEmpty
        {
            get
            {
                if (Actives.Count == 0 && Inactives.Count == 0) return true;
                else return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.Common.ConditionItems
{
    public class Active
    {
        public Active(string flag)
        {
            Flag = flag;
        }
        public string Flag { get; set; }
        public override string ToString()
        {
            return Config.GlobalStateClass + "." + Flag;
        }
    }
}

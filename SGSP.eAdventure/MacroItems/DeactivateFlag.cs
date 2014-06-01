using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.MacroItems
{
    public class DeactivateFlag : MacroItem
    {
        public DeactivateFlag(string flag)
        {
            Flag = flag;
        }
        public string Flag { get; set; }
    }
}

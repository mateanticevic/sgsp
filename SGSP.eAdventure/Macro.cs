using SGSP.eAdventure.MacroItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure
{
    public class Macro : BaseElement
    {
        public Macro()
        {
            Actions = new List<MacroItem>();
        }
        public List<MacroItem> Actions { get; set; }
    }
}

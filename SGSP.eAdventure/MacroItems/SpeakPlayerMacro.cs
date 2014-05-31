using SGSP.eAdventure.ConversationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.MacroItems
{
    public class SpeakPlayerMacro : MacroItem
    {
        public SpeakPlayerMacro(string text)
        {
            SpeakPlayer = text;
        }
        public string SpeakPlayer { get; set; }
    }
}

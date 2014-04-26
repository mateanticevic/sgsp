using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.ConversationItems
{
    public class SpeakPlayer : Speak
    {
        public SpeakPlayer()
        {
            Guid = Guid.NewGuid();
        }
    }
}

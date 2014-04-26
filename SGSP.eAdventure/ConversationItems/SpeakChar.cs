using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.ConversationItems
{
    public class SpeakChar : Speak
    {
        public SpeakChar()
        {
            Guid = Guid.NewGuid();
        }
        public string TargetId { get; set; }
    }
}

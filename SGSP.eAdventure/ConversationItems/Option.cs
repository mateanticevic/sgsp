using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.ConversationItems
{
    public class Option
    {
        public ConversationNode NextNode { get; set; }
        public string NextNodeId { get; set; }
        public SpeakPlayer SelectedOption { get; set; }
    }
}

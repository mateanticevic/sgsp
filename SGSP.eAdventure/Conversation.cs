using SGSP.eAdventure.ConversationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure
{
    public class Conversation : BaseElement
    {
        public Conversation()
        {
            Nodes = new List<ConversationNode>();
        }
        public List<ConversationNode> Nodes { get; set; }
    }
}

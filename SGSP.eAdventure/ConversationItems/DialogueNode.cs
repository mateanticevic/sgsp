using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.ConversationItems
{
    public class DialogueNode : ConversationNode
    {
        public DialogueNode()
        {
            Dialogue = new List<Speak>();
        }

        public List<Speak> Dialogue { get; set; }
        public ConversationNode NextNode { get; set; }
        public string NextNodeId { get; set; }

        public string GetStartId()
        {
            if (Dialogue.Count != 0) return Dialogue[0].Id;
            else if (NextNode != null) return ((DialogueNode)NextNode).GetStartId();
            else if (EndEffect != null) return "Effect" + NodeIndex;

            return "var end";
        }
    }
}

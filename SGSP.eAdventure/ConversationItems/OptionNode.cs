using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.ConversationItems
{
    public class OptionNode : ConversationNode
    {
        private Guid Guid;
        public OptionNode()
        {
            Options = new List<Option>();
            Guid = Guid.NewGuid();
        }
        public List<Option> Options { get; set; }
        public string Id
        {
            get
            {
                return "Option" +  Guid.ToString().Substring(0, 5);
            }
        }
    }
}

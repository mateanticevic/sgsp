using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.ConversationItems
{
    public class ConversationNode
    {
        public string NodeIndex { get; set; }

        public Effect EndEffect { get; set; }

        public string PropertyEndEffect
        {
            get
            {
                return "Effect" + NodeIndex;
            }
        }
    }
}

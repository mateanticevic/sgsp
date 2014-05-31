using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.ConversationItems
{
    public class Speak
    {

        public Guid Guid { get; set; }

        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value.Replace("\"", "'");
            }
        }

        public string Id
        {
            get
            {
                return "Speak" +  Guid.ToString().Substring(0, 5);
            }
        }
    }
}

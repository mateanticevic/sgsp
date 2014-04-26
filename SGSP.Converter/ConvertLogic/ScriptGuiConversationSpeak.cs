using SGSP.eAdventure.ConversationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptGuiConversationSpeak
    {
        private string speak;
        private string text;

        public ScriptGuiConversationSpeak(Speak speak)
        {
            this.text = speak.Text;
            this.speak = speak.Id;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{speak}", speak);
            rpl.Add("{text}", text);
            rpl.Add("{width}", text.Length.ToString());

            return Generator.Snippet(Resources.Snippet.GuiConversationSpeak, rpl);
        }
    }
}

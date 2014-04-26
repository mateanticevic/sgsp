using SGSP.eAdventure.ConversationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptGuiSpeakPlayer
    {
        private string id;
        private string text;

        public ScriptGuiSpeakPlayer(SpeakPlayer speak)
        {
            id = speak.Id;
            this.text = speak.Text;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{id}", id);
            rpl.Add("{text}", text);

            return Generator.Snippet(Resources.Snippet.GuiSpeakPlayer, rpl);
        }
    }
}

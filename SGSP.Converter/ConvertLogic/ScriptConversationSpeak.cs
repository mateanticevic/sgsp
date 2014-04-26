using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptConversationSpeak
    {
        private string thisSpeak;
        private string nextSpeak;
        private string endEffect;
        public ScriptConversationSpeak(string thisSpeak, string nextSpeak)
        {
            this.nextSpeak = nextSpeak;
            this.thisSpeak = thisSpeak;
        }

        public ScriptConversationSpeak(string thisSpeak, string nextSpeak, string endEffect)
        {
            this.nextSpeak = nextSpeak;
            this.thisSpeak = thisSpeak;
            this.endEffect = endEffect;
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            if (nextSpeak == null) rpl.Add("{nextSpeak}", endEffect);
            else rpl.Add("{nextSpeak}", nextSpeak);
            rpl.Add("{thisSpeak}", thisSpeak);

            return Generator.Snippet(Resources.Snippet.ConversationSpeak, rpl);
        }
    }
}

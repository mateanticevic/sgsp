using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptShowGui
    {
        private string id;
        private string action;
        private string icon;

        private int left;
        private int down;

        public ScriptShowGui(string objectId, string actions)
        {
            id = objectId;
            action = actions;

            left = 0;
            down = 0;
        }

        public ScriptShowGui(string objectId, string actions, ActionTypes type)
        {
            id = objectId;
            action = actions;

            if(type == ActionTypes.Custom)
            {
                left = 70;
                icon = "custom";
            }
            else if (type == ActionTypes.Examine)
            {
                icon = "examine";
            }
            else if (type == ActionTypes.TalkTo)
            {
                down = 70;
                icon = "talkto";
            }
            else if (type == ActionTypes.Use)
            {
                left = 70;
                down = 70;
                icon = "use";
            }
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();

            rpl.Add("{id}", id);
            rpl.Add("{action}", action);
            rpl.Add("{icon}", icon);

            rpl.Add("{down}", down.ToString());
            rpl.Add("{left}", left.ToString());

            return Generator.Snippet(Resources.Snippet.ShowGui, rpl);
        }
        
    }
}

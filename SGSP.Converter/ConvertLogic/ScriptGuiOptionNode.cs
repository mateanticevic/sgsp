using SGSP.eAdventure.ConversationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptGuiOptionNode
    {

        private OptionNode option;

        public ScriptGuiOptionNode(OptionNode node)
        {
            option = node;

        }


        private string GenerateOptions()
        {

            List<string> options = new List<string>();

            foreach (var item in option.Options)
            {
                Dictionary<string, string> rpl = new Dictionary<string, string>();

                var next = ((DialogueNode)item.NextNode).Dialogue;


                rpl.Add("{next}", ((DialogueNode)item.NextNode).GetStartId());
                rpl.Add("{text}", item.SelectedOption.Text);
                rpl.Add("{optionNode}", option.Id);
                rpl.Add("{width}", item.SelectedOption.Text.Length.ToString());
                rpl.Add("{index}", option.Options.IndexOf(item).ToString());

                options.Add(Generator.Snippet(Resources.Snippet.GuiConversationOption, rpl));
            }

            

            return String.Join("\r\n", options);
        }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string,string>();


            rpl.Add("{condition}", option.Id);
            rpl.Add("{action}", GenerateOptions());

            return Generator.Snippet(Resources.Snippet.If, rpl);
        }
    }
}

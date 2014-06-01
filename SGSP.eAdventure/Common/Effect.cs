using SGSP.eAdventure.Common.ConditionItems;
using SGSP.eAdventure.ConversationItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.Common
{
    public class Effect
    {

        public Effect()
        {
            Condition = new Condition();
            SetFlags = new Condition();

            SpeakPlayer = new List<SpeakPlayer>();
        }

        public Condition SetFlags { get; set; }
        public List<SpeakPlayer> SpeakPlayer { get; set; }
        public Condition Condition { get; set; }
        public Scene TriggerScene { get; set; }
        public SlideScene TriggerSlideScene { get; set; }
        public string TriggerSceneId { get; set; }
        public string TriggerSlideSceneId { get; set; }
        public string TriggerConversationId { get; set; }
    }
}

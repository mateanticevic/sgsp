using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.MacroItems
{
    public class TriggerSceneMacro : MacroItem
    {
        public TriggerSceneMacro(string sceneId)
        {
            TargetSceneId = sceneId;
        }

        public string TargetSceneId { get; set; }
    }
}

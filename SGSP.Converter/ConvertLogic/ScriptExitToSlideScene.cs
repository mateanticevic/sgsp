using SGSP.Converter.ConvertLogic.Conditions;
using SGSP.eAdventure;
using SGSP.eAdventure.Common;
using SGSP.eAdventure.SceneItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptExitToSlideScene
    {
        private string ss;
        private string condition;
        private string exId;
        private string effects;
        private Transform transform;

        public ScriptExitToSlideScene(Exit exit, string exitId)
        {
            ss = exit.TargetObjectId;
            condition = IfGenerator.Generate(exit.Condition);
            this.transform = exit.Transform;
            exId = exitId;
        }


        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{ss}", ss);
            rpl.Add("{condition}", condition);
            rpl.Add("{exitId}", exId);
            rpl.Add("{effect}", effects);

            rpl.Add("{x}", transform.X.ToString());
            rpl.Add("{y}", transform.Y.ToString());
            rpl.Add("{width}", transform.Width.ToString());
            rpl.Add("{height}", transform.Height.ToString());

            return Generator.Snippet(Resources.Snippet.ExitToSlideScene, rpl);
        }
    }
}

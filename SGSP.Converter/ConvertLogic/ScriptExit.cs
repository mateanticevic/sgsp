using SGSP.Converter.ConvertLogic.Conditions;
using SGSP.eAdventure.Common;
using SGSP.eAdventure.SceneItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptExit
    {
        private string scene;
        private string condition;
        private string exId;
        private string effects;
        private Transform transform;
        private Exit exit;

        public ScriptExit(Exit exit, string exitId)
        {
            this.exit = exit;
            scene = exit.TargetObjectId;
            condition = IfGenerator.Generate(exit.Condition);
            transform = exit.Transform;
            exId = exitId;
            
        }

        public ScriptExit(Exit exit, string exitId, string effectsPayload)
        {
            this.exit = exit;
            scene = exit.TargetObjectId;
            condition = IfGenerator.Generate(exit.Condition);
            transform = exit.Transform;
            exId = exitId;
            effects = effectsPayload;
        }

        public bool SceneDoesntChange { get; set; }

        public override string ToString()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{scene}", scene);
            rpl.Add("{condition}", condition);
            rpl.Add("{exitId}", exId);
            rpl.Add("{effect}", effects);

            if (SceneDoesntChange) rpl.Add("{sameScene}", "true");
            else rpl.Add("{sameScene}", "false");

            if (exit.Effect != null && exit.Effect.SpeakPlayer.Count != 0) rpl.Add("{speak}", exit.Effect.SpeakPlayer[0].Id);
            else rpl.Add("{speak}", "var ok");

            rpl.Add("{x}", transform.X.ToString());
            rpl.Add("{y}", transform.Y.ToString());
            rpl.Add("{width}", transform.Width.ToString());
            rpl.Add("{height}", transform.Height.ToString());

            return Generator.Snippet(Resources.Snippet.Exit, rpl);
        }
    }
}

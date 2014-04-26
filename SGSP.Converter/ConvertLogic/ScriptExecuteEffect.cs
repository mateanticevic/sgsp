using SGSP.Converter.CodeResources.Snippets;
using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter.ConvertLogic
{
    class ScriptExecuteEffect
    {
            private Effect effect;
            private string effectId;

            public ScriptExecuteEffect(Effect effect, string effectId)
            {
                this.effect = effect;
                this.effectId = effectId;
            }

            public override string ToString()
            {
                Dictionary<string, string> rpl = new Dictionary<string, string>();

                rpl.Add("{effectId}", effectId);

                var slideSceneActivate = String.Empty;
                if (effect.TriggerSlideScene != null) slideSceneActivate = CodeUtility.SetVar(effect.TriggerSlideScene.PropertyActive, true);
                rpl.Add("{slideSceneActivate}", slideSceneActivate);

                var sceneLoad = String.Empty;
                if (effect.TriggerScene != null) sceneLoad = CodeUtility.SceneLoad(effect.TriggerSceneId);
                rpl.Add("{sceneLoad}", sceneLoad);

                var setFlags = String.Empty;
                if(!effect.SetFlags.IsEmpty)
                {
                    foreach (var item in effect.SetFlags.Actives)
                    {
                        setFlags += CodeUtility.SetVar(item.ToString(), true) + "\r\n";
                    }

                    foreach (var item in effect.SetFlags.Inactives)
                    {
                        setFlags += CodeUtility.SetVar(item.ToString(), false) + "\r\n";
                    }
                }
                rpl.Add("{setFlags}", setFlags);

                
                return Generator.Snippet(Resources.Snippet.ExecuteEffect, rpl);
            }

        }
}

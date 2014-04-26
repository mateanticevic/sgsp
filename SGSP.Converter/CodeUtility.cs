using SGSP.Converter.CodeResources.Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.Converter
{
    class CodeUtility
    {
        public static string SetVar(string name, string value)
        {
            return CodeTemplate.SetVariable.Replace("{var}", name).Replace("{val}", value);
        }

        public static string SetVar(string name, bool value)
        {
            if (value) return CodeTemplate.SetVariable.Replace("{var}", name).Replace("{val}", "true");
            return CodeTemplate.SetVariable.Replace("{var}", name).Replace("{val}", "false");
        }

        public static string SceneLoad(string scene)
        {
            return CodeTemplate.LoadScene.Replace("{scene}", scene);
        }

        public static string GameObjectAddComponent(string gameObject, string activity)
        {
            return CodeTemplate.GameObjectAddComponent.Replace("{object}", gameObject).Replace("{component}", activity);
        }
    }
}

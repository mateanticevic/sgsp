using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.ConvertLogic.Conditions
{
    class IfGenerator
    {
        public static string Generate(Condition c)
        {

            if (c == null) return "true";

            if (c.Actives.Count == 0 && c.Inactives.Count != 0)
            {
                string inactives = string.Join(" && ", c.Inactives.Select(x => x.ToString()).ToArray());

                return inactives;
            }

            else if (c.Actives.Count != 0 && c.Inactives.Count == 0)
            {
                string actives = string.Join(" && ", c.Actives.Select(x => x.ToString()).ToArray());

                return actives;
            }

            else if (c.Actives.Count != 0 && c.Inactives.Count != 0)
            {
                string actives = string.Join(" && ", c.Actives.Select(x => x.ToString()).ToArray());
                string inactives = string.Join(" && ", c.Inactives.Select(x => x.ToString()).ToArray());

                return actives + " && " + inactives;
            }

            return "true";
        }
    }
}

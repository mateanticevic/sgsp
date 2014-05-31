using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.Common.ConditionItems
{
    public class Inactive
    {
        public Inactive(string flag)
        {
            Flag = flag;
        }
        private string flag;
        public string Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value.Replace("-", String.Empty);
            }
        }

        public override string ToString()
        {
            return "!" + Config.GlobalStateClass + "." + Flag;
        }
    }
}

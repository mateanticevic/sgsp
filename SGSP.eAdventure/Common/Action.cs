using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.Common
{
    public class Action
    {
        public Effect Effect { get; set; }
    }

    public enum ActionTypes
    {
        Custom,
        Examine,
        TalkTo,
        Use
    }
}

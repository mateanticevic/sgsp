using SGSP.eAdventure.Common;
using SGSP.eAdventure.Common.ConditionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.SceneItems
{
    public class Exit : ObjectWithCondition
    {
        public Exit()
        {
            Transform = new Transform();
        }

        public Effect Effect { get; set; }
        public string ExitLook { get; set; }
        public string MouseOverDescription { get; set; }
        public bool IsRectangular { get; set; }
        public BaseElement TargetObject { get; set; }
        public string TargetObjectId { get; set; }
        public Transform Transform { get; set; }
    }
}

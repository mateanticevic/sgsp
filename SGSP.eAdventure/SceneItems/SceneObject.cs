using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.SceneItems
{
    public class SceneObject
    {
        public Condition Condition { get; set; }
        public int Layer { get; set; }
        public Object TargetObject { get; set; }
        public string TargetId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

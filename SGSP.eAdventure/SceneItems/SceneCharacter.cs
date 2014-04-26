using SGSP.eAdventure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSP.eAdventure.SceneItems
{
    public class SceneCharacter
    {
        public Condition Condition { get; set; }
        public Character Character { get; set; }
        public string CharacterId { get; set; }
        public decimal Scale { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}

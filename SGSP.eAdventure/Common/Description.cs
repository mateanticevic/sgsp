using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure.Common
{
    public class Description
    {
        public string Name { get; set; }
        public string Brief { get; set; }
        public string Detailed { get; set; }

        public override string ToString()
        {
            if (Brief == String.Empty) return Name;

            return Brief;
        }
    }
}

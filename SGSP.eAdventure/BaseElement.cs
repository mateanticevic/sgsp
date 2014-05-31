using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure
{
    public class BaseElement
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value.Replace("-", String.Empty);
            }
        }
    }
}

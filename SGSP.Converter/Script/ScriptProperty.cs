using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.Script
{
    class ScriptProperty
    {
        public ScriptProperty(string name, string type)
        {
            Name = name;
            Type = type;

            IsPublic = false;
        }
        public ScriptProperty(string nm, string ty, bool st)
        {
            IsStatic = st;
            Name = nm;
            Type = ty;

            IsPublic = false;
        }
        public bool IsStatic { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public override string ToString()
        {
            string prop = "public static {0} {1};";

            if (!IsPublic) prop = prop.Replace("public ", String.Empty);
            if (!IsStatic) prop = prop.Replace("static ", String.Empty);

            return String.Format(prop, Type, Name);
        }
    }
}

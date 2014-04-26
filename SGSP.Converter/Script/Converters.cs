using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.Script
{
    class Converters
    {
        public static string ReturnTypeToString(ReturnType type)
        {
            Dictionary<ReturnType, string> dict = new Dictionary<ReturnType, string>();

            dict.Add(ReturnType.Void, "void");
            dict.Add(ReturnType.String, "string");
            dict.Add(ReturnType.Int, "int");
            dict.Add(ReturnType.Float, "float");
            dict.Add(ReturnType.Bool, "bool");

            return dict[type];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.Script
{
    public enum ReturnType
    {
        Void,
        String,
        Int,
        Float,
        Bool
    }

    public enum MethodType
    {
        Static,
        NonStatic
    }

    public enum ScriptType
    {
        Activity,
        Main
    }
}

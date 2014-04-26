using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SGSP.Converter
{
    class Generator
    {
        public static string Snippet(string snippet, Dictionary<string, string> replacements)
        {
            StreamReader sr = new StreamReader(Resources.Settings.PathCodeSnippets + snippet);
            string data = sr.ReadToEnd();

            sr.Close();

            foreach (KeyValuePair<string, string> item in replacements)
            {
                data = data.Replace(item.Key, item.Value);
            }

            return data;
        }
    }
}

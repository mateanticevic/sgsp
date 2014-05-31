using SGSP.Converter.CodeResources.Snippets;
using SGSP.Converter.Script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SGSP.Converter.Script
{
    class UnityScript
    {
        public UnityScript(ScriptType type)
        {
            ScriptName = "Script";

            Methods = new List<ScriptMethod>();
            Using = new List<string>();
            Properties = new List<ScriptProperty>();
            Type = type;

            UsingAddDefaults();
        }

        private string scriptName;

        public List<ScriptMethod> Methods { get; set; }
        public List<ScriptProperty> Properties { get; set; }
        public string ScriptName
        {
            get
            {
                return scriptName;
            }
            set
            {
                scriptName = value.Replace("-", String.Empty); ;
            }
        }
        public ScriptType Type { get; set; }
        public List<String> Using { get; set; }

        public bool AddProperty(ScriptProperty prop)
        {
            if (Properties.Exists(x => x.Name == prop.Name)) return false;

            Properties.Add(prop);

            return true;
        }

        public string Generate()
        {
            string output = GenerateUsing() + GenerateRoot();
            return output;
        }

        private string GenerateUsing()
        {
            string output = "";

            foreach (var item in Using)
            {
                output += CodeTemplate.Using.Replace("{namespace}", item) + "\r\n";
            }

            output += "\r\n";

            return output;
        }

        private string GenerateRoot()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            rpl.Add("{scriptName}", ScriptName);
            rpl.Add("{content}", GenerateMethods());
            rpl.Add("{properties}", GeneratePropeties());

            return Generator.Snippet(Resources.Snippet.Class, rpl);
        }

        private string GeneratePropeties()
        {
            string output = "";

            foreach (var item in Properties)
            {
                output += item.ToString() + "\r\n";
            }

            return output;
        }

        private string GenerateMethods()
        {
            string output = "";

            Methods.ForEach(x => output += x.Generate() + "\r\n");

            return output;
        }

        public ScriptMethod GetMethod(string methodName)
        {
            var existing = Methods.Where(x => x.Name == methodName);

            if (existing.Count() != 0) return existing.First();
            else
            {
                var method = new ScriptMethod(methodName);
                Methods.Add(method);

                return method;
            }
        }

        private void UsingAddDefaults()
        {
            Using.Add("UnityEngine");
            Using.Add("UnityEditor");
            Using.Add("System.Collections");
        }
    }
}

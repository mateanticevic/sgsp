using SGSP.Converter.CodeResources.Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.Converter.Script
{
    class ScriptMethod
    {

        public ScriptMethod(string name)
        {
            Name = name;
            Returns = ReturnType.Void;
            Type = MethodType.NonStatic;
            CodeChunks = new List<CodeChunk>();
        }
        public string Name { get; set; }
        public ReturnType Returns { get; set; }
        public MethodType Type { get; set; }
        public bool IsMenuItem { get; set; }
        public bool IsStatic { get; set; }
        public bool IsPublic { get; set; }
        public List<CodeChunk> CodeChunks { get; set; }
        public string Payload
        {
            get
            {
                return String.Join("\r\n", CodeChunks.OrderByDescending(x => x.Priority).Select(x => x.Code).ToList());
            }
        }



        public string GetCall()
        {
            return String.Format("{0}();", Name);
        }

        public string Generate()
        {
            Dictionary<string, string> rpl = new Dictionary<string, string>();

            if (Returns == ReturnType.Int) rpl.Add("{returns}", "int");
            else rpl.Add("{returns}", "void");

            if (IsPublic) rpl.Add("{public}", "public");
            else rpl.Add("{public}", "private");

            var payload = Payload;

            payload = "\t" + payload;
            payload = payload.Replace("\r\n", "\r\n\t");

            rpl.Add("{name}", Name);
            rpl.Add("{payload}", payload);

            if (IsMenuItem) rpl.Add("{menu}", CodeTemplate.UnityMenuItem.Replace("{name}", Name));
            else rpl.Add("{menu}", String.Empty);

            if (IsStatic) rpl.Add("{static}", "static");
            else rpl.Add("{static}", String.Empty);

            return Generator.Snippet(Resources.Snippet.Method, rpl);
        }
    }
}

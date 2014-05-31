using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure
{
    public class Chapter
    {
        public Chapter()
        {
            Characters = new List<Character>();
            Conversations = new List<Conversation>();
            Macros = new List<Macro>();
            Objects = new List<Object>();
            Scenes = new List<Scene>();
            SlideScenes = new List<SlideScene>();
        }

        public List<Character> Characters { get; set; }
        public List<Conversation> Conversations { get; set; }
        public List<Macro> Macros { get; set; }
        public List<Object> Objects { get; set; }
        public List<Scene> Scenes { get; set; }
        public List<SlideScene> SlideScenes { get; set; }

        public BaseElement GetElementById(string id)
        {
            var o = Objects.Where(x => x.Id == id);
            var s = Scenes.Where(x => x.Id == id);
            var ss = SlideScenes.Where(x => x.Id == id);

            if (o.Count() != 0) return o.First();
            if (s.Count() != 0) return s.First();
            if (ss.Count() != 0) return ss.First();

            return null;
        }
    }
}

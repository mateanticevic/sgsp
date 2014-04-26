using SGSP.eAdventure.Common;
using SGSP.eAdventure.SceneItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure
{
    public class Scene : BaseElement
    {
        public Scene()
        {
            ActiveAreas = new List<ActiveArea>();
            Characters = new List<SceneCharacter>();
            Conversations = new List<Conversation>();
            Resources = new List<ResourceList>();
            Exits = new List<Exit>();
            Objects = new List<SceneObject>();
        }
        public List<SceneCharacter> Characters { get; set; }
        public List<Conversation> Conversations { get; set; }
        public string SceneId { get; set; }
        public List<ActiveArea> ActiveAreas { get; set; }
        public List<Exit> Exits { get; set; }
        public List<ResourceList> Resources { get; set; }
        public List<SceneObject> Objects { get; set; }
    }
}

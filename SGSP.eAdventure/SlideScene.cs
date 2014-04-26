using SGSP.eAdventure.SceneItems.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.eAdventure
{
    public class SlideScene : BaseElement
    {
        public SlideScene()
        {
            Slides = new List<Asset>();
        }
        public Scene TargetScene { get; set; }
        public string Name { get; set; }
        public List<Asset> Slides { get; set; }
        public string PropertyActive
        {
            get
            {
                return "slideSceneActive" + this.Id;
            }
        }
        public string PropertyStarted
        {
            get
            {
                return "slideSceneStarted" + this.Id;
            }
        }
    }
}

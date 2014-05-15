using SGSP.eAdventure;
using SGSP.eAdventure.Common;
using SGSP.eAdventure.Common.ConditionItems;
using SGSP.eAdventure.ConversationItems;
using SGSP.eAdventure.SceneItems;
using SGSP.eAdventure.SceneItems.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SGSP.Converter.ModelCreator
{
    public class Model
    {
        public static string Path = "Fire/";

        public static Chapter CreateChapter(string xmlPath)
        {
            Chapter chapter = new Chapter();
            
            XElement xml = XElement.Load(xmlPath);

            #region Objects

            foreach (XElement ele in xml.Elements("object"))
            {
                eAdventure.Object obj = new eAdventure.Object();
                chapter.Objects.Add(obj);

                obj.Id = ele.Attribute("id").Value;

                XElement desc = ele.Element("description");

                obj.Description = CreateDescription(desc);

                #region Resources

                foreach (XElement resourceEle in ele.Elements("resources"))
                {
                    ResourceList resList = new ResourceList();
                    obj.Resources.Add(resList);

                    foreach (XElement assetEle in resourceEle.Elements("asset"))
                    {
                        Asset asset = new Asset();
                        resList.Assets.Add(asset);

                        asset.Type = assetEle.Attribute("type").Value;
                        asset.Uri = assetEle.Attribute("uri").Value;
                    }

                    XElement conditionEle = resourceEle.Element("condition");

                    if (conditionEle != null) resList.Condition = CreateCondition(conditionEle);
                }

                #endregion

                #region Actions
                try
                {
                    foreach (var action in ele.Element("actions").Elements())
                    {
                        if (action.Name == "use")
                        {
                            Use use = new Use();
                            obj.Use = use;

                            Effect effect = new Effect();
                            var effectEle = action.Element("effect");

                            if (effectEle != null) use.Effect = CreateEffect(effectEle);
                        }
                    }
                }
                catch { }
                #endregion
            }

            #endregion

            #region Scenes

            foreach (XElement sceneEle in xml.Elements("scene"))
            {
                Scene scene = new Scene();
                chapter.Scenes.Add(scene);

                scene.Id = sceneEle.Attribute("id").Value;

                #region ActiveAreas

                var aaEle = sceneEle.Element("active-areas");

                if(aaEle != null)
                {
                    foreach (var aa in aaEle.Elements("active-area"))
                    {
                        ActiveArea a = new ActiveArea();
                        scene.ActiveAreas.Add(a);

                        a.Id = aa.Attribute("id").Value;
                        a.Description = CreateDescription(aa.Element("description"));
                        a.Transform = CreateTransform(aa);

                        foreach (var action in aa.Element("actions").Elements())
                        {
                            if (action.Name == "use")
                            {
                                Use use = new Use();
                                a.Use = use;

                                Effect effect = new Effect();
                                var effectEle = action.Element("effect");

                                if (effectEle != null) use.Effect = CreateEffect(effectEle);
                            }
                        }
                    }
                }

                #endregion

                #region Resources

                foreach (XElement resourceEle in sceneEle.Elements("resources"))
                {

                    ResourceList resList = new ResourceList();
                    scene.Resources.Add(resList);

                    foreach (XElement assetEle in resourceEle.Elements("asset"))
                    {
                        Asset asset = new Asset();
                        resList.Assets.Add(asset);

                        asset.Type = assetEle.Attribute("type").Value;
                        asset.Uri = assetEle.Attribute("uri").Value;
                    }

                    XElement conditionEle = resourceEle.Element("condition");

                    if (conditionEle != null) resList.Condition = CreateCondition(conditionEle);
                }

                #endregion

                #region Characters


                var sceneCharacters = sceneEle.Element("characters");

                if(sceneCharacters != null)
                {
                    foreach (XElement item in sceneCharacters.Elements("character-ref"))
                    {
                        SceneCharacter sChar = new SceneCharacter();
                        scene.Characters.Add(sChar);

                        sChar.CharacterId = item.Attribute("idTarget").Value;
                        sChar.Scale = Convert.ToDecimal(item.Attribute("scale").Value);
                        sChar.X = Convert.ToInt32(item.Attribute("x").Value);
                        sChar.Y = Convert.ToInt32(item.Attribute("y").Value);

                        sChar.Condition = CreateCondition(item.Element("condition"));
                    }
                }

                #endregion

                #region Exits

                foreach (XElement exitEle in sceneEle.Element("exits").Elements("exit"))
                {
                    Exit exit = new Exit();
                    scene.Exits.Add(exit);

                    exit.Transform = CreateTransform(exitEle);

                    exit.IsRectangular = exitEle.Attribute("rectangular").Value == "yes" ? true : false;
                    exit.MouseOverDescription = exitEle.Element("exit-look").Attribute("text").Value;
                    exit.TargetObjectId = exitEle.Attribute("idTarget").Value;

                    XElement conditionEle = exitEle.Element("condition");
                    if (conditionEle != null) exit.Condition = CreateCondition(conditionEle);
                    
                    var effect = exitEle.Element("effect");
                    if (effect != null) exit.Effect = CreateEffect(effect);
                }

                #endregion

                #region Objects

                if (sceneEle.Element("objects") != null)
                {
                    foreach (var objectEle in sceneEle.Element("objects").Elements("object-ref"))
                    {
                        SceneObject obj = new SceneObject();
                        scene.Objects.Add(obj);

                        obj.TargetId = objectEle.Attribute("idTarget").Value;
                        obj.TargetObject = chapter.Objects.Single(x => x.Id == obj.TargetId);
                        obj.X = Convert.ToInt32(objectEle.Attribute("x").Value);
                        obj.Y = Convert.ToInt32(objectEle.Attribute("y").Value);
                    }
                }

                #endregion

            }

            #endregion

            #region Slidescenes

            foreach (XElement slideEle in xml.Elements("slidescene"))
            {
                SlideScene ss = new SlideScene();
                chapter.SlideScenes.Add(ss);

                ss.Name = slideEle.Element("name").Value;
                ss.Id = slideEle.Attribute("id").Value;
                // ss.TargetScene = chapter.Scenes.First(x => x.SceneId == slideEle.Attribute("idTarget").Value);

                string slidesPath = slideEle.Element("resources").Element("asset").Attribute("uri").Value;

                int slideNumber = 1;

                while(true)
                {
                    string uri = slidesPath + "_0" + slideNumber.ToString();
                    if (!File.Exists(Path + uri + ".jpg")) break;

                    slideNumber++;

                    Asset newSlide = new Asset();
                    ss.Slides.Add(newSlide);

                    newSlide.Type = "slides";
                    newSlide.Uri = uri;
                }
            }

            #endregion

            #region Conversations

            foreach (var convEle in xml.Elements("graph-conversation"))
            {
                Conversation conv = new Conversation();
                chapter.Conversations.Add(conv);

                conv.Id = convEle.Attribute("id").Value;

                foreach (var node in convEle.Elements())
                {

                    #region Dialogue Node

                    if (node.Name == "dialogue-node")
                    {
                        DialogueNode dNode = new DialogueNode();
                        conv.Nodes.Add(dNode);

                        dNode.NodeIndex = node.Attribute("nodeindex").Value;

                        var end = node.Element("end-conversation");

                        if(end != null)
                        {
                            dNode.EndEffect = CreateEffect(end.Element("effect"));
                        }

                        foreach (var item in node.Elements())
                        {
                            if (item.Name == "speak-char")
                            {
                                SpeakChar speak = new SpeakChar();
                                dNode.Dialogue.Add(speak);

                                speak.TargetId = item.Attribute("idTarget").Value;
                                speak.Text = item.Value;
                            }
                            else if (item.Name == "speak-player")
                            {
                                SpeakPlayer speak = new SpeakPlayer();
                                dNode.Dialogue.Add(speak);

                                speak.Text = item.Value;
                            }
                            else if (item.Name == "child")
                            {
                                dNode.NextNodeId = item.Attribute("nodeindex").Value;
                            }
                        }
                    }

                    #endregion

                    #region Option Node

                    else if(node.Name == "option-node")
                    {
                        OptionNode oNode = new OptionNode();
                        conv.Nodes.Add(oNode);

                        oNode.NodeIndex = node.Attribute("nodeindex").Value;

                        foreach (var optionEle in node.Elements("speak-player"))
                        {
                            Option option = new Option();
                            oNode.Options.Add(option);

                            SpeakPlayer speak = new SpeakPlayer();
                            option.SelectedOption = speak;

                            speak.Text = optionEle.Value;

                            option.NextNodeId = optionEle.ElementsAfterSelf("child").First().Attribute("nodeindex").Value;
                        }
                    }

                    #endregion

                }
            }

            #endregion

            #region Characters

            foreach (var charEle in xml.Elements("character"))
            {
                Character character = new Character();
                chapter.Characters.Add(character);

                character.Id = charEle.Attribute("id").Value;

                foreach (var item in charEle.Element("resources").Elements("asset"))
                {
                    Asset asset = new Asset();
                    character.Assets.Add(asset);

                    asset.Type = item.Attribute("type").Value;
                    asset.Uri = item.Attribute("uri").Value;
                }

                character.Description = CreateDescription(charEle.Element("description"));

                character.TextColor = CreateTextColor(charEle.Element("textcolor"));
            }

            #endregion


            #region Second loop

            #region Scenes

            foreach (var scene in chapter.Scenes)
            {
                #region Exits

                foreach (var exit in scene.Exits)
                {
                    exit.TargetObject = chapter.GetElementById(exit.TargetObjectId);
                }

                #endregion

                #region Active areas

                foreach (var area in scene.ActiveAreas)
                {
                    if(area.Use != null)
                    {
                        if(area.Use.Effect != null)
                        {
                            var effect = area.Use.Effect;
                            effect.TriggerSlideScene = (SlideScene) chapter.GetElementById(effect.TriggerSlideSceneId);
                            effect.TriggerScene = (Scene) chapter.GetElementById(effect.TriggerSceneId);

                            if(effect.TriggerConversationId != null)
                            {
                                effect.TriggerScene.Conversations.Add(chapter.Conversations.Single(x => x.Id == effect.TriggerConversationId));
                            }
                        }
                    }
                }

                #endregion

                #region Characters

                foreach (var item in scene.Characters)
                {
                    item.Character = chapter.Characters.Single(x => x.Id == item.CharacterId);
                }

                #endregion
            }

            #endregion

            #region Objects

            foreach (var obj in chapter.Objects)
            {
                try
                {
                    if (obj.Use.Effect.TriggerSlideSceneId != null) obj.Use.Effect.TriggerSlideScene = chapter.SlideScenes.Single(x => x.Id == obj.Use.Effect.TriggerSlideSceneId);
                    if (obj.Use.Effect.TriggerSceneId != null) obj.Use.Effect.TriggerScene = chapter.Scenes.Single(x => x.Id == obj.Use.Effect.TriggerSceneId); 
                }
                catch { }
            }

            #endregion

            #region Conversations

            foreach (var item in chapter.Conversations)
            {
                foreach (var node in item.Nodes)
                {
                    var endEffect = node.EndEffect;
                    if(endEffect != null)
                    {
                        if (endEffect.TriggerSlideSceneId != null) endEffect.TriggerSlideScene = chapter.SlideScenes.Single(x => x.Id == endEffect.TriggerSlideSceneId);
                        if (endEffect.TriggerSceneId != null) endEffect.TriggerScene = chapter.Scenes.Single(x => x.Id == endEffect.TriggerSceneId);
                    }

                    if(node is DialogueNode)
                    {
                        var current = ((DialogueNode)node);
                        if(current.NextNodeId != null) current.NextNode = item.Nodes.Single(x => x.NodeIndex == current.NextNodeId);
                    }
                    else
                    {
                        var current = ((OptionNode)node);
                        foreach (var option in current.Options)
                        {
                            if(option.NextNodeId != null) option.NextNode = item.Nodes.Single(x => x.NodeIndex == option.NextNodeId); 
                        }
                    }
                }
            }

            #endregion

            #endregion


            return chapter;
        }

        public static Condition CreateCondition(XElement ele)
        {
            if (ele == null) return null;

            Condition condition = new Condition();

            foreach (XElement active in ele.Elements("active"))
            {
                string flag = active.Attribute("flag").Value;
                condition.Actives.Add(new Active(flag));
            }

            foreach (XElement inactive in ele.Elements("inactive"))
            {
                string flag = inactive.Attribute("flag").Value;
                condition.Inactives.Add(new Inactive(flag));
            }

            return condition;
        }

        public static Description CreateDescription(XElement ele)
        {
            Description x = new Description();

            x.Brief = ele.Element("brief").Value;
            x.Detailed = ele.Element("detailed").Value;
            x.Name = ele.Element("name").Value;

            return x;
        }

        private static Effect CreateEffect(XElement ele)
        {
            Effect effect = new Effect();


            var ssTrigger = ele.Element("trigger-cutscene");
            if (ssTrigger != null) effect.TriggerSlideSceneId = ssTrigger.Attribute("idTarget").Value;

            var sTrigger = ele.Element("trigger-scene");
            if (sTrigger != null) effect.TriggerSceneId = sTrigger.Attribute("idTarget").Value;

            var convTrigger = ele.Element("trigger-conversation");
            if (convTrigger != null)
            {
                effect.TriggerConversationId = convTrigger.Attribute("idTarget").Value;

                if(ssTrigger != null)
                {
                    
                }
                else
                {

                }
            }

            foreach (var activeEle in ele.Elements("activate"))
            {
                Active active = new Active(activeEle.Attribute("flag").Value);
                effect.SetFlags.Actives.Add(active);
            }

            foreach (var inactiveEle in ele.Elements("deactivate"))
            {
                Inactive inactive = new Inactive(inactiveEle.Attribute("flag").Value);
                effect.SetFlags.Inactives.Add(inactive);
            }

            var speak = ele.Element("speak-player");
            if (speak != null) effect.SpeakPlayer = new SpeakPlayer() { Text = speak.Value };

            return effect;
        }

        public static Transform CreateTransform(XElement ele)
        {
            Transform x = new Transform();

            x.X = Convert.ToInt32(ele.Attribute("x").Value);
            x.Y = Convert.ToInt32(ele.Attribute("y").Value);
            x.Height = Convert.ToInt32(ele.Attribute("height").Value);
            x.Width = Convert.ToInt32(ele.Attribute("width").Value);

            return x;
        }

        public static TextColor CreateTextColor(XElement ele)
        {
            TextColor x = new TextColor();

            x.BorderColor = ele.Element("bordercolor").Attribute("color").Value;
            x.BubbleBkgColor = ele.Attribute("bubbleBkgColor").Value;
            x.BubbleBorderColor = ele.Attribute("bubbleBorderColor").Value;
            x.FontColor = ele.Element("frontcolor").Attribute("color").Value;

            return x;
        }
    }
}

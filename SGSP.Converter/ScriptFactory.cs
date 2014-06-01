using SGSP.Converter.CodeResources.Snippets;
using SGSP.Converter.ConvertLogic;
using SGSP.Converter.ConvertLogic.Conditions;
using SGSP.Converter.Resources;
using SGSP.Converter.Script;
using SGSP.Converter.UnityProject;
using SGSP.Converter.Utility;
using SGSP.eAdventure;
using SGSP.eAdventure.Common;
using SGSP.eAdventure.ConversationItems;
using SGSP.eAdventure.SceneItems.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SGSP.Converter
{
    public class ScriptFactory
    {
        UnityScript state;
        UnityScript script;

        private string xmlRootPath;

        List<UnityScript> activityScripts = new List<UnityScript>();

        public ScriptFactory(string path)
        {
            xmlRootPath = path;
        }

        public void GenerateScript(Chapter chapter)
        {
            script = new UnityScript(ScriptType.Main);
            state = new UnityScript(ScriptType.Main);

            state.ScriptName = Config.GlobalStateClass;

            script.Using.Add("System.Collections.Generic");          

            ScriptMethod camMethod = new ScriptMethod(Settings.CameraDefault);
            script.Methods.Add(camMethod);

            camMethod.IsStatic = true;
            camMethod.CodeChunks.Add(new CodeChunk(1, new ScriptCameraDefault().ToString()));
            
            
            #region Scenes

            foreach (var scene in chapter.Scenes)
            {
                ScriptMethod method = new ScriptMethod(scene.SceneId);
                script.Methods.Add(method);

                method.Name = scene.Id;
                method.IsMenuItem = true;
                method.IsStatic = true;


                method.CodeChunks.Add(new CodeChunk(1, script.GetMethod(Settings.CameraDefault).GetCall()));

                UnityScript activity = new UnityScript(ScriptType.Activity);
                activity.ScriptName = scene.Id + "Background0";

                activityScripts.Add(activity);

                var update = activity.GetMethod(Unity.MethodUpdate);
                var start = activity.GetMethod(Unity.MethodStart);
                var onGui = activity.GetMethod(Unity.MethodOnGUI);


                #region Resources

                foreach (var res in scene.Resources)
                {
                    string resId = scene.Resources.IndexOf(res).ToString();

                    var existingActivity = activityScripts.Where(x => x.ScriptName == scene.Id + "Background" + resId);
                    var rActivity = existingActivity.Count() != 0 ? existingActivity.First() : new UnityScript(ScriptType.Activity);

                    var rStart = rActivity.GetMethod(Unity.MethodStart);
                    var rUpdate = rActivity.GetMethod(Unity.MethodUpdate);


                    if (existingActivity.Count() == 0)
                    {
                        activityScripts.Add(rActivity);
                        rActivity.ScriptName = scene.Id + "Background" + resId;
                    }

                    foreach (Asset asset in res.Assets)
                    {
                        if (asset.Type == Element.AssetBackground)
                        {
                            method.CodeChunks.Add(new CodeChunk(1, new ScriptBackground(resId, asset.UriUnityFormat).ToString()));
                            method.CodeChunks.Add(new CodeChunk(1, CodeUtility.GameObjectAddComponent("bg" + resId.ToString(), rActivity.ScriptName)));

                            if(res.Condition != null) DeclareConditionProperties(res.Condition);

                            rUpdate.CodeChunks.Add(new CodeChunk(100, new ScriptVisibility(IfGenerator.Generate(res.Condition)).ToString()));
                        }

                        if (asset.Type == Element.AssetBackgroundMusic)
                        {
                            rActivity.Properties.Add(new ScriptProperty("bgMusic", "AudioSource", false));

                            rStart.CodeChunks.Add(new CodeChunk(1, new ScriptBackgroundMusicInit(asset.UriUnityFormat).ToString()));
                            rUpdate.CodeChunks.Add(new CodeChunk(100, new ScriptBackgroundMusicControl(IfGenerator.Generate(res.Condition)).ToString()));
                        }
                    }
                }

                #endregion

                #region Exits

                foreach (var exit in scene.Exits)
                {

                    if (exit.Effect != null) GenerateEffect(activity, exit.Effect);


                    if(exit.Condition != null) DeclareConditionProperties(exit.Condition);

                    var exitId = scene.Exits.IndexOf(exit).ToString();



                    activity.AddProperty(new ScriptProperty("OverExit" + exitId, "bool"));

                    onGui.CodeChunks.Add(new CodeChunk(1, new ScriptOverExit(exit.MouseOverDescription, exitId).ToString()));

                    #region Target Scene/SlideScene

                    if (exit.TargetObject is Scene)
                    {
                        ScriptExit scriptExit;
                        if (exit.Effect != null) scriptExit = new ScriptExit(exit, exitId, GenerateEffectActions(exit.Effect));
                        else scriptExit = new ScriptExit(exit, exitId);

                        if (exit.TargetObjectId == scene.Id) scriptExit.SceneDoesntChange = true;

                        update.CodeChunks.Add(new CodeChunk(1, scriptExit.ToString()));
                    }
                    else if(exit.TargetObject is SlideScene)
                    {
                        update.CodeChunks.Add(new CodeChunk(1, new ScriptExitToSlideScene(exit, exitId).ToString()));

                        var triggerSS = (SlideScene) exit.TargetObject;
                        activity.AddProperty(new ScriptProperty(triggerSS.PropertyActive, "bool"));
                        activity.AddProperty(new ScriptProperty(triggerSS.PropertyStarted, "bool"));


                        var scriptSsStarted = new ScriptSlideSceneStarted(triggerSS.Id, script.ScriptName).ToString();
                        var scriptSsActive= new ScriptSlideSceneActive(triggerSS.Id, script.ScriptName).ToString();

                        update.CodeChunks.Add(new CodeChunk(10, scriptSsStarted));
                        update.CodeChunks.Add(new CodeChunk(10, scriptSsActive));
                    }

                    #endregion

                    
                }


                #endregion

                #region ActiveAreas
                foreach (var area in scene.ActiveAreas)
                {
                    if (area.Use != null)
                    {
                        activity.AddProperty(new ScriptProperty(area.Id + "GuiPosition", "Vector3"));
                        activity.AddProperty(new ScriptProperty(area.Id + "GuiActive", "bool"));                        

                        var payloadActions = GenerateEffectActions(area.Use.Effect);

                        if (area.Use.Effect != null)
                        {
                            GenerateEffect(activity, area.Use.Effect);

                            if (area.Use.Effect.SpeakPlayer.Count != 0) payloadActions += CodeUtility.SetVar(area.Use.Effect.SpeakPlayer[0].Id, true);
                        }



                        var triggerSS = area.Use.Effect.TriggerSlideScene;
                        if (triggerSS != null)
                        {
                            payloadActions += CodeUtility.SetVar(triggerSS.PropertyActive, true);
                            AddSlideScene(activity, triggerSS);
                        }

                        var triggerS = area.Use.Effect.TriggerScene;
                        if (triggerS != null) payloadActions += CodeUtility.SceneLoad(triggerS.Id);

                        activity.AddProperty(new ScriptProperty("OverExit" + area.Id, "bool"));

                        onGui.CodeChunks.Add(new CodeChunk(1, new ScriptOverExit(area.Description.Name, area.Id).ToString()));
                        onGui.CodeChunks.Add(new CodeChunk(1, new ScriptShowGui(area.Id, payloadActions).ToString()));

                        

                        if (area.Transform != null) update.CodeChunks.Add(new CodeChunk(10, new ScriptActivateGuiConditioned(area.Id, area.Transform).ToString()));
                        else update.CodeChunks.Add(new CodeChunk(10, new ScriptActivateGui(area.Id).ToString()));
                    }
                }

                #endregion

                #region Objects

                foreach (var obj in scene.Objects)
                {

                    Transform boundingBox = new Transform();

                    #region Resources

                   
                    foreach (var res in obj.TargetObject.Resources)
                    {
                        foreach (var asset in res.Assets)
                        {
                            if(asset.Type == "image")
                            {
                                boundingBox = PngUtility.GetBoundingBox((Bitmap)Bitmap.FromFile(Path.GetDirectoryName(xmlRootPath) + "\\" + asset.Uri));

                                var imageId = Path.GetFileNameWithoutExtension(asset.Uri);
                                var gameObject = imageId + "Image";

                                method.CodeChunks.Add(new CodeChunk(1, new ScriptObjectImage(imageId, asset.UriUnityFormat).ToString()));

                                script.AddProperty(new ScriptProperty(gameObject, "GameObject", true));

                                UnityScript oBehavior = new UnityScript(ScriptType.Activity);
                                oBehavior.ScriptName = imageId + "Behaviour";

                                activityScripts.Add(oBehavior);

                                var oUpdate = oBehavior.GetMethod(Unity.MethodUpdate);
                                var visibilityCondition = res.Condition == null ? obj.Condition : res.Condition;
                                oUpdate.CodeChunks.Add(new CodeChunk(10, new ScriptVisibility(IfGenerator.Generate(visibilityCondition)).ToString()));

                                method.CodeChunks.Add(new CodeChunk(0, CodeTemplate.GameObjectAddComponent.Replace("{object}", gameObject).Replace("{component}", oBehavior.ScriptName)));

                                DeclareConditionProperties(res.Condition);
                                DeclareConditionProperties(obj.Condition);
                            }
                        }
                    }

                    #endregion

                    #region Actions

                    var targetObject = obj.TargetObject;

                    foreach (var action in targetObject.Actions)
                    {
                        activity.Properties.Add(new ScriptProperty(targetObject.Id + "GuiPosition", "Vector3"));
                        activity.Properties.Add(new ScriptProperty(targetObject.Id + "GuiActive", "bool"));

                        string payloadActions = CodeUtility.SetVar("State.WearingGloves", true);

                        activity.AddProperty(new ScriptProperty("OverExit" + targetObject.Id, "bool"));
                        onGui.CodeChunks.Add(new CodeChunk(1, new ScriptShowGui(targetObject.Id, payloadActions).ToString()));


                        update.CodeChunks.Add(new CodeChunk(1, new ScriptActivateGuiConditioned(targetObject.Id, boundingBox).ToString()));
                    }

                    if (obj.TargetObject.Use != null)
                    {
                        activity.Properties.Add(new ScriptProperty(targetObject.Id + "GuiPosition", "Vector3"));
                        activity.Properties.Add(new ScriptProperty(targetObject.Id + "GuiActive", "bool"));

                        var triggerSS = targetObject.Use.Effect.TriggerSlideScene;

                        var payloadActions = GenerateEffectActions(targetObject.Use.Effect);

                        if (triggerSS != null) payloadActions += CodeUtility.SetVar(triggerSS.PropertyActive, true);

                        activity.AddProperty(new ScriptProperty("OverExit" + targetObject.Id, "bool"));
                        onGui.CodeChunks.Add(new CodeChunk(1, new ScriptShowGui(targetObject.Id, payloadActions).ToString()));


                        update.CodeChunks.Add(new CodeChunk(1, new ScriptActivateGuiConditioned(targetObject.Id, boundingBox).ToString()));

                        if (triggerSS != null) AddSlideScene(activity, triggerSS);
                    }

                    #endregion

                    #region Over

                    activity.AddProperty(new ScriptProperty(obj.TargetObject.PropertyOver, "bool"));

                    onGui.CodeChunks.Add(new CodeChunk(5, new ScriptOverObject(obj.TargetObject.Description.Brief, obj.TargetObject.PropertyOver).ToString()));
                    update.CodeChunks.Add(new CodeChunk(5, new ScriptIsOver(obj.TargetObject.PropertyOver, boundingBox).ToString()));

                    #endregion

                }

                #endregion

                #region Conversations


                foreach (var conv in scene.Conversations)
                {
                    foreach (var node in conv.Nodes)
                    {

                       if(node is DialogueNode)
                       {
                           var dialogue = (DialogueNode)node;

                           var endEffect = node.EndEffect;
                           if (endEffect != null)
                           {
                               var triggerSS = endEffect.TriggerSlideScene;
                               if (triggerSS != null) AddSlideScene(activity, triggerSS);
                           }

                           if (dialogue.EndEffect != null)
                           {
                               update.CodeChunks.Add(new CodeChunk(5, new ScriptExecuteEffect(dialogue.EndEffect, dialogue.PropertyEndEffect).ToString()));

                               activity.AddProperty(new ScriptProperty(dialogue.PropertyEndEffect, "bool"));

                               if (!dialogue.EndEffect.SetFlags.IsEmpty) DeclareConditionProperties(dialogue.EndEffect.SetFlags);
                           }

                           foreach (var speak in dialogue.Dialogue)
                           {
                               activity.AddProperty(new ScriptProperty(speak.Id, "bool"));

                               string nextSpeak = null;

                               
                               if (dialogue.Dialogue.Last() == speak)
                               {
                                   if(dialogue.NextNodeId != null)
                                   {
                                       var nextNode = conv.Nodes.Single(x => x.NodeIndex == dialogue.NextNodeId);

                                       if (nextNode is OptionNode) nextSpeak = ((OptionNode)nextNode).Id;
                                       else nextSpeak = ((DialogueNode)nextNode).Dialogue[0].Id;
                                   }
                               }
                               else nextSpeak = dialogue.Dialogue[dialogue.Dialogue.IndexOf(speak) + 1].Id;



                               if (dialogue.Dialogue.Last() == speak) update.CodeChunks.Add(new CodeChunk(8, new ScriptConversationSpeak(speak.Id, nextSpeak, dialogue.PropertyEndEffect).ToString()));
                               else update.CodeChunks.Add(new CodeChunk(8, new ScriptConversationSpeak(speak.Id, nextSpeak).ToString()));
                             
                               onGui.CodeChunks.Add(new CodeChunk(1, new ScriptGuiConversationSpeak(speak).ToString()));
                           }
                       }
                       else
                       {
                           var option = (OptionNode)node;

                           activity.AddProperty(new ScriptProperty(option.Id, "bool"));

                           onGui.CodeChunks.Add(new CodeChunk(1,new ScriptGuiOptionNode(option).ToString()));


                       }
                    }
                }

                #endregion

                #region Characters

                //foreach (var sChar in scene.Characters)
                //{

                //    update.CodeChunks.Add(new CodeChunk(100, new ScriptVisibility(IfGenerator.Generate(sChar.Condition)).ToString()));

                //    foreach (var asset in sChar.Character.Assets)
                //    {
                //        if(asset.Type == Element.AssetStandUp)
                //        {
                //            var imageId = Path.GetFileNameWithoutExtension(asset.Uri);
                //            var gameObject = imageId + "Image";

                //            PngUtility.GetBoundingBox((Bitmap)Bitmap.FromFile(Path.GetDirectoryName(xmlRootPath) + "\\" + asset.Uri + "_01.png"));

                //            script.Properties.Add(new ScriptProperty(gameObject, "GameObject", true));
                //            method.CodeChunks.Add(new CodeChunk(1, new ScriptObjectImage(imageId, asset.Uri).ToString()));
                //        }
                //    }
                //}

                #endregion

            }

            #endregion

            #region SlideScenes

            foreach (var ss in chapter.SlideScenes)
            {
                script.Properties.Add(new ScriptProperty("slides" + ss.Id, "List<GameObject>", true));

                ScriptMethod method = new ScriptMethod("StartSlideScene" + ss.Id);
                script.Methods.Add(method);

                method.IsStatic = true;
                method.IsPublic = true;

                string payload = "";

                payload = CodeTemplate.InitSlideList.Replace("{id}", ss.Id) + "\r\n";

                int sn = 1;

                foreach (var slide in ss.Slides)
                {
                    payload += new ScriptAddSlideScene(ss.Id, sn, slide.Uri).ToString() + "\r\n";
                    sn++;
                }

                method.CodeChunks.Add(new CodeChunk(1, payload));

                ScriptMethod next = new ScriptMethod("NextSlideScene" + ss.Id);
                script.Methods.Add(next);

                next.Returns = ReturnType.Int;
                next.IsStatic = true;
                next.IsPublic = true;

                next.CodeChunks.Add(new CodeChunk(1, new ScriptNextSlideScene(ss.Id).ToString()));
            }

            #endregion

            #region Output writer

            StreamWriter sw;

            sw = new StreamWriter("UnityScripts/" + state.ScriptName + ".cs");
            sw.Write(state.Generate());
            sw.Close();

            sw = new StreamWriter("UnityScripts/" + script.ScriptName + ".cs");
            sw.Write(script.Generate());
            sw.Close();

            foreach (var item in activityScripts)
            {
                sw = new StreamWriter("UnityScripts/" + item.ScriptName + ".cs");
                sw.Write(item.Generate());
                sw.Close();
            }

            #endregion

            #region Unity project

            UnityProjectUtility.CreateUnityProject(chapter, xmlRootPath);

            #endregion

        }

        private void DeclareConditionProperties(Condition c)
        {
            if (c == null || c.IsEmpty) return;

            foreach (var condition in c.Inactives)
            {
                ScriptProperty property = new ScriptProperty(condition.Flag, "bool", false);
                property.IsPublic = true;
                property.IsStatic = true;

                if (!state.Properties.Exists(x => x.Name == condition.Flag)) state.Properties.Add(property);
            }

            foreach (var condition in c.Actives)
            {
                ScriptProperty property = new ScriptProperty(condition.Flag, "bool", false);
                property.IsPublic = true;
                property.IsStatic = true;

                if (!state.Properties.Exists(x => x.Name == condition.Flag)) state.Properties.Add(property);
            }


        }

        private string GenerateEffectActions(Effect effect)
        {
            string output = "";

            if (effect.SetFlags != null) DeclareConditionProperties(effect.SetFlags);

            foreach (var item in effect.SetFlags.Actives)
            {
                output += CodeTemplate.SetVariable.Replace("{var}", item.ToString()).Replace("{val}", CodeTemplate.True) + "\r\n";
            }

            foreach (var item in effect.SetFlags.Inactives)
            {
                output += CodeTemplate.SetVariable.Replace("{var}", item.ToString()).Replace("{val}", CodeTemplate.False) + "\r\n";
            }

            return output;
        }

        private void GenerateEffect(UnityScript activity, Effect effect)
        {
            if(effect.SpeakPlayer.Count != 0)
            {
                var speak = effect.SpeakPlayer;

                var onGui = activity.GetMethod("OnGUI");
                var payload = new ScriptGuiSpeakPlayer(speak[0]).ToString();
                onGui.CodeChunks.Add(new CodeChunk(1, payload));

                var update = activity.GetMethod("Update");
                var setBool = CodeTemplate.SetVariable.Replace("{var}", speak[0].Id).Replace("{val}", "false");

                var turnOff = new ScriptIf(new ScriptIf(setBool, "Input.GetMouseButtonDown(0)").ToString(), speak[0].Id).ToString();

                update.CodeChunks.Add(new CodeChunk(5, turnOff));

                activity.Properties.Add(new ScriptProperty(speak[0].Id, "bool", false));
            }
        }

        private void AddSlideScene(UnityScript activity, SlideScene ss)
        {
            activity.AddProperty(new ScriptProperty(ss.PropertyActive, "bool", false));
            activity.AddProperty(new ScriptProperty(ss.PropertyStarted, "bool", false));

            var update = activity.GetMethod("Update");

            update.CodeChunks.Add(new CodeChunk(10, new ScriptSlideSceneStarted(ss.Id, script.ScriptName).ToString()));
            update.CodeChunks.Add(new CodeChunk(10, new ScriptSlideSceneActive(ss.Id, script.ScriptName).ToString()));
        }
    }
}

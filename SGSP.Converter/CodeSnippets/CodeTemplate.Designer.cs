﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGSP.Converter.CodeResources.Snippets {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CodeTemplate {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CodeTemplate() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SGSP.Converter.CodeSnippets.CodeTemplate", typeof(CodeTemplate).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to false.
        /// </summary>
        internal static string False {
            get {
                return ResourceManager.GetString("False", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {object}.AddComponent(&quot;{component}&quot;);.
        /// </summary>
        internal static string GameObjectAddComponent {
            get {
                return ResourceManager.GetString("GameObjectAddComponent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to slides{id} = new List&lt;GameObject&gt; ();.
        /// </summary>
        internal static string InitSlideList {
            get {
                return ResourceManager.GetString("InitSlideList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Application.LoadLevel(&quot;{scene}&quot;);.
        /// </summary>
        internal static string LoadScene {
            get {
                return ResourceManager.GetString("LoadScene", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {var} = {val};.
        /// </summary>
        internal static string SetVariable {
            get {
                return ResourceManager.GetString("SetVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to true.
        /// </summary>
        internal static string True {
            get {
                return ResourceManager.GetString("True", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [MenuItem (&quot;eAdventure/Create scene/{name}&quot;)].
        /// </summary>
        internal static string UnityMenuItem {
            get {
                return ResourceManager.GetString("UnityMenuItem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to using {namespace};.
        /// </summary>
        internal static string Using {
            get {
                return ResourceManager.GetString("Using", resourceCulture);
            }
        }
    }
}
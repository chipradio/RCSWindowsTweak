﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsTweak {
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
    internal class PowerShellScripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PowerShellScripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WindowsTweak.PowerShellScripts", typeof(PowerShellScripts).Assembly);
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
        ///   Looks up a localized string similar to Disable-ScheduledTask -TaskName &quot;ScheduledDefrag&quot; -TaskPath &quot;\Microsoft\Windows\Defrag\&quot;.
        /// </summary>
        internal static string DisableDefrag {
            get {
                return ResourceManager.GetString("DisableDefrag", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Set-NetAdapterAdvancedProperty -Name  {0} -DisplayName &quot;Flow Control&quot; DisplayValue &quot;Disable&quot;.
        /// </summary>
        internal static string DisableFlowControlOnNics {
            get {
                return ResourceManager.GetString("DisableFlowControlOnNics", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Disable-NetAdapterManagement -Name *.
        /// </summary>
        internal static string DisablePowerManagementOnNics {
            get {
                return ResourceManager.GetString("DisablePowerManagementOnNics", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Disable-NetAdapterQos -Name {0}.
        /// </summary>
        internal static string DisableQOSOnNics {
            get {
                return ResourceManager.GetString("DisableQOSOnNics", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Disable-NetAdapterBinding -Name &quot; {0} -ComponentID &quot;ms_tcpip6&quot;.
        /// </summary>
        internal static string DisableTCPIP6OnNics {
            get {
                return ResourceManager.GetString("DisableTCPIP6OnNics", resourceCulture);
            }
        }
    }
}
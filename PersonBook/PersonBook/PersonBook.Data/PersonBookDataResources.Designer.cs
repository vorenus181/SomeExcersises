﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonBook.Data {
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
    internal class PersonBookDataResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PersonBookDataResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PersonBook.Data.PersonBookDataResources", typeof(PersonBookDataResources).Assembly);
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
        ///   Looks up a localized string similar to Write failed..
        /// </summary>
        internal static string DataAccess_FailedMessage {
            get {
                return ResourceManager.GetString("DataAccess_FailedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Write succeded..
        /// </summary>
        internal static string DataAccess_SuccessMessage {
            get {
                return ResourceManager.GetString("DataAccess_SuccessMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Birth date.
        /// </summary>
        internal static string Person_BirthDate {
            get {
                return ResourceManager.GetString("Person_BirthDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Marital status.
        /// </summary>
        internal static string Person_MaritalStatus {
            get {
                return ResourceManager.GetString("Person_MaritalStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name.
        /// </summary>
        internal static string Person_Name {
            get {
                return ResourceManager.GetString("Person_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Surname.
        /// </summary>
        internal static string Person_Surname {
            get {
                return ResourceManager.GetString("Person_Surname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creation Date.
        /// </summary>
        internal static string SerializableEntity_CreationDate {
            get {
                return ResourceManager.GetString("SerializableEntity_CreationDate", resourceCulture);
            }
        }
    }
}

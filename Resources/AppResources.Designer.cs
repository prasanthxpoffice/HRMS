using System;
using System.Resources;
using System.Globalization;

namespace HRMS.Resources {
    public class AppResources {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal AppResources() {
        }

        public static ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    resourceMan = new ResourceManager("HRMS.Resources.AppResources", typeof(AppResources).Assembly);
                }
                return resourceMan;
            }
        }

        public static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        public static string Gender_NameEn_Required => ResourceManager.GetString("Gender_NameEn_Required", resourceCulture);
        public static string Gender_NameAr_Required => ResourceManager.GetString("Gender_NameAr_Required", resourceCulture);
        public static string Error_TooLong => ResourceManager.GetString("Error_TooLong", resourceCulture);
    }
}

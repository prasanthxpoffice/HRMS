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

        public static string Company_NameEn_Required => ResourceManager.GetString("Company_NameEn_Required", resourceCulture);
        public static string Company_NameAr_Required => ResourceManager.GetString("Company_NameAr_Required", resourceCulture);

        public static string Position_NameEn_Required => ResourceManager.GetString("Position_NameEn_Required", resourceCulture);
        public static string Position_NameAr_Required => ResourceManager.GetString("Position_NameAr_Required", resourceCulture);

        public static string LeaveType_NameEn_Required => ResourceManager.GetString("LeaveType_NameEn_Required", resourceCulture);
        public static string LeaveType_NameAr_Required => ResourceManager.GetString("LeaveType_NameAr_Required", resourceCulture);

        public static string ContractType_NameEn_Required => ResourceManager.GetString("ContractType_NameEn_Required", resourceCulture);
        public static string ContractType_NameAr_Required => ResourceManager.GetString("ContractType_NameAr_Required", resourceCulture);

        public static string Holiday_NameEn_Required => ResourceManager.GetString("Holiday_NameEn_Required", resourceCulture);
        public static string Holiday_NameAr_Required => ResourceManager.GetString("Holiday_NameAr_Required", resourceCulture);

        public static string CompanyContract_Company_Required => ResourceManager.GetString("CompanyContract_Company_Required", resourceCulture);
        public static string CompanyContract_Contract_Required => ResourceManager.GetString("CompanyContract_Contract_Required", resourceCulture);
        public static string SelectCompany => ResourceManager.GetString("SelectCompany", resourceCulture);
        public static string SelectContract => ResourceManager.GetString("SelectContract", resourceCulture);

        public static string CompanyPosition_Company_Required => ResourceManager.GetString("CompanyPosition_Company_Required", resourceCulture);
        public static string CompanyPosition_Position_Required => ResourceManager.GetString("CompanyPosition_Position_Required", resourceCulture);
        public static string SelectPosition => ResourceManager.GetString("SelectPosition", resourceCulture);
        public static string Religion => ResourceManager.GetString("Religion", resourceCulture);
        public static string Nationality => ResourceManager.GetString("Nationality", resourceCulture);
        public static string ShiftType => ResourceManager.GetString("ShiftType", resourceCulture);
        public static string Gender => ResourceManager.GetString("Gender", resourceCulture);
        public static string Holiday => ResourceManager.GetString("Holiday", resourceCulture);
        public static string Company => ResourceManager.GetString("Company", resourceCulture);
        public static string Id => ResourceManager.GetString("Id", resourceCulture);
        public static string All => ResourceManager.GetString("All", resourceCulture);
        public static string Add => ResourceManager.GetString("Add", resourceCulture);
        public static string Edit => ResourceManager.GetString("Edit", resourceCulture);
        public static string Delete => ResourceManager.GetString("Delete", resourceCulture);
        public static string Save => ResourceManager.GetString("Save", resourceCulture);
        public static string Cancel => ResourceManager.GetString("Cancel", resourceCulture);
        public static string Search => ResourceManager.GetString("Search", resourceCulture);
        public static string NoRecordsFound => ResourceManager.GetString("NoRecordsFound", resourceCulture);
        public static string Actions => ResourceManager.GetString("Actions", resourceCulture);
        public static string CreatedDate => ResourceManager.GetString("CreatedDate", resourceCulture);
        public static string SelectReligion => ResourceManager.GetString("SelectReligion", resourceCulture);
        public static string SelectNationality => ResourceManager.GetString("SelectNationality", resourceCulture);
        public static string SelectShiftType => ResourceManager.GetString("SelectShiftType", resourceCulture);
        public static string HolidayDetails => ResourceManager.GetString("HolidayDetails", resourceCulture);
        public static string FromDate => ResourceManager.GetString("FromDate", resourceCulture);
        public static string ToDate => ResourceManager.GetString("ToDate", resourceCulture);
        public static string RequiredField => ResourceManager.GetString("RequiredField", resourceCulture);
        public static string InvalidDateRange => ResourceManager.GetString("InvalidDateRange", resourceCulture);
        public static string HolidayOverlapError => ResourceManager.GetString("HolidayOverlapError", resourceCulture);
    }
}

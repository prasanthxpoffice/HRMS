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

        public static string Language_English => ResourceManager.GetString("Language_English", resourceCulture);
        public static string Language_Arabic => ResourceManager.GetString("Language_Arabic", resourceCulture);
        public static string Previous => ResourceManager.GetString("Previous", resourceCulture);
        public static string Next => ResourceManager.GetString("Next", resourceCulture);
        public static string Rows => ResourceManager.GetString("Rows", resourceCulture);
        public static string Actions => ResourceManager.GetString("Actions", resourceCulture);
        public static string Search => ResourceManager.GetString("Search", resourceCulture);
        public static string NoRecordsFound => ResourceManager.GetString("NoRecordsFound", resourceCulture);
        public static string ShowingXToYOfZ => ResourceManager.GetString("ShowingXToYOfZ", resourceCulture);
        public static string Id => ResourceManager.GetString("Id", resourceCulture);
        public static string NameEn => ResourceManager.GetString("NameEn", resourceCulture);
        public static string NameAr => ResourceManager.GetString("NameAr", resourceCulture);
        public static string Gender_Title => ResourceManager.GetString("Gender_Title", resourceCulture);
        public static string Gender_Description => ResourceManager.GetString("Gender_Description", resourceCulture);
        public static string Gender_GridTitle => ResourceManager.GetString("Gender_GridTitle", resourceCulture);
        public static string Gender_Add => ResourceManager.GetString("Gender_Add", resourceCulture);
        public static string PageTitle_Home => ResourceManager.GetString("PageTitle_Home", resourceCulture);
        public static string PageTitle_Genders => ResourceManager.GetString("PageTitle_Genders", resourceCulture);
        public static string Category => ResourceManager.GetString("Category", resourceCulture);
        public static string CreatedDate => ResourceManager.GetString("CreatedDate", resourceCulture);
        public static string All => ResourceManager.GetString("All", resourceCulture);
        public static string Add => ResourceManager.GetString("Add", resourceCulture);
        public static string Edit => ResourceManager.GetString("Edit", resourceCulture);
        public static string Delete => ResourceManager.GetString("Delete", resourceCulture);
        public static string Save => ResourceManager.GetString("Save", resourceCulture);
        public static string Cancel => ResourceManager.GetString("Cancel", resourceCulture);
        public static string AdminDashboard => ResourceManager.GetString("AdminDashboard", resourceCulture);
        public static string WelcomeBack => ResourceManager.GetString("WelcomeBack", resourceCulture);
        public static string SystemStatus => ResourceManager.GetString("SystemStatus", resourceCulture);
        public static string DatabaseConnected => ResourceManager.GetString("DatabaseConnected", resourceCulture);
        public static string AuthenticationActive => ResourceManager.GetString("AuthenticationActive", resourceCulture);
        public static string ManageGenders => ResourceManager.GetString("ManageGenders", resourceCulture);
        public static string Settings => ResourceManager.GetString("Settings", resourceCulture);
        public static string SystemDashboard => ResourceManager.GetString("SystemDashboard", resourceCulture);
        public static string DashboardDescription => ResourceManager.GetString("DashboardDescription", resourceCulture);
        public static string BulkDeleteSuccess => ResourceManager.GetString("BulkDeleteSuccess", resourceCulture);
        public static string BulkDeleteError => ResourceManager.GetString("BulkDeleteError", resourceCulture);
        public static string DeleteSelected => ResourceManager.GetString("DeleteSelected", resourceCulture);
        public static string DeleteConfirmTitle => ResourceManager.GetString("DeleteConfirmTitle", resourceCulture);
        public static string DeleteConfirmMessage => ResourceManager.GetString("DeleteConfirmMessage", resourceCulture);
        public static string BulkDeleteConfirmMessage => ResourceManager.GetString("BulkDeleteConfirmMessage", resourceCulture);
        public static string Error_Required => ResourceManager.GetString("Error_Required", resourceCulture);
        public static string Error_TooLong => ResourceManager.GetString("Error_TooLong", resourceCulture);
        public static string Male => ResourceManager.GetString("Male", resourceCulture);
        public static string Female => ResourceManager.GetString("Female", resourceCulture);
        public static string Loading => ResourceManager.GetString("Loading", resourceCulture);
        public static string Gender_NameEn_Required => ResourceManager.GetString("Gender_NameEn_Required", resourceCulture);
        public static string Gender_NameAr_Required => ResourceManager.GetString("Gender_NameAr_Required", resourceCulture);
        public static string DatabaseError => ResourceManager.GetString("DatabaseError", resourceCulture);
        public static string SystemError => ResourceManager.GetString("SystemError", resourceCulture);
        public static string ViewDetails => ResourceManager.GetString("ViewDetails", resourceCulture);
        public static string HideDetails => ResourceManager.GetString("HideDetails", resourceCulture);
        public static string UnexpectedError => ResourceManager.GetString("UnexpectedError", resourceCulture);
        public static string UnexpectedErrorDesc => ResourceManager.GetString("UnexpectedErrorDesc", resourceCulture);
        public static string Reload => ResourceManager.GetString("Reload", resourceCulture);
        public static string Gender => ResourceManager.GetString("Gender", resourceCulture);
        public static string Company => ResourceManager.GetString("Company", resourceCulture);
        public static string Close => ResourceManager.GetString("Close", resourceCulture);
        public static string ExportSuccess => ResourceManager.GetString("ExportSuccess", resourceCulture);
        public static string SelectFormat => ResourceManager.GetString("SelectFormat", resourceCulture);
        public static string Company_NameEn_Required => ResourceManager.GetString("Company_NameEn_Required", resourceCulture);
        public static string Company_NameAr_Required => ResourceManager.GetString("Company_NameAr_Required", resourceCulture);
        public static string Company_GridTitle => ResourceManager.GetString("Company_GridTitle", resourceCulture);
        public static string Company_Add => ResourceManager.GetString("Company_Add", resourceCulture);
        public static string Position_NameEn_Required => ResourceManager.GetString("Position_NameEn_Required", resourceCulture);
        public static string Position_NameAr_Required => ResourceManager.GetString("Position_NameAr_Required", resourceCulture);
        public static string Position_GridTitle => ResourceManager.GetString("Position_GridTitle", resourceCulture);
        public static string Position_Add => ResourceManager.GetString("Position_Add", resourceCulture);
        public static string Position => ResourceManager.GetString("Position", resourceCulture);
        public static string LeaveType_NameEn_Required => ResourceManager.GetString("LeaveType_NameEn_Required", resourceCulture);
        public static string LeaveType_NameAr_Required => ResourceManager.GetString("LeaveType_NameAr_Required", resourceCulture);
        public static string LeaveType_GridTitle => ResourceManager.GetString("LeaveType_GridTitle", resourceCulture);
        public static string LeaveType_Add => ResourceManager.GetString("LeaveType_Add", resourceCulture);
        public static string LeaveType => ResourceManager.GetString("LeaveType", resourceCulture);
        public static string ContractType_NameEn_Required => ResourceManager.GetString("ContractType_NameEn_Required", resourceCulture);
        public static string ContractType_NameAr_Required => ResourceManager.GetString("ContractType_NameAr_Required", resourceCulture);
        public static string ContractType_GridTitle => ResourceManager.GetString("ContractType_GridTitle", resourceCulture);
        public static string ContractType_Add => ResourceManager.GetString("ContractType_Add", resourceCulture);
        public static string ContractType => ResourceManager.GetString("ContractType", resourceCulture);
        public static string Holiday_NameEn_Required => ResourceManager.GetString("Holiday_NameEn_Required", resourceCulture);
        public static string Holiday_NameAr_Required => ResourceManager.GetString("Holiday_NameAr_Required", resourceCulture);
        public static string Holiday_GridTitle => ResourceManager.GetString("Holiday_GridTitle", resourceCulture);
        public static string Holiday_Add => ResourceManager.GetString("Holiday_Add", resourceCulture);
        public static string Holiday => ResourceManager.GetString("Holiday", resourceCulture);
        public static string CompanyContract_GridTitle => ResourceManager.GetString("CompanyContract_GridTitle", resourceCulture);
        public static string CompanyContract_Add => ResourceManager.GetString("CompanyContract_Add", resourceCulture);
        public static string CompanyContract => ResourceManager.GetString("CompanyContract", resourceCulture);
        public static string CompanyContract_Company_Required => ResourceManager.GetString("CompanyContract_Company_Required", resourceCulture);
        public static string CompanyContract_Contract_Required => ResourceManager.GetString("CompanyContract_Contract_Required", resourceCulture);
        public static string SelectCompany => ResourceManager.GetString("SelectCompany", resourceCulture);
        public static string SelectContract => ResourceManager.GetString("SelectContract", resourceCulture);
        public static string CompanyPosition_GridTitle => ResourceManager.GetString("CompanyPosition_GridTitle", resourceCulture);
        public static string CompanyPosition_Add => ResourceManager.GetString("CompanyPosition_Add", resourceCulture);
        public static string CompanyPosition => ResourceManager.GetString("CompanyPosition", resourceCulture);
        public static string CompanyPosition_Company_Required => ResourceManager.GetString("CompanyPosition_Company_Required", resourceCulture);
        public static string CompanyPosition_Position_Required => ResourceManager.GetString("CompanyPosition_Position_Required", resourceCulture);
        public static string SelectPosition => ResourceManager.GetString("SelectPosition", resourceCulture);
        public static string SelectHoliday => ResourceManager.GetString("SelectHoliday", resourceCulture);
        public static string SelectGender => ResourceManager.GetString("SelectGender", resourceCulture);
        public static string Religion => ResourceManager.GetString("Religion", resourceCulture);
        public static string Nationality => ResourceManager.GetString("Nationality", resourceCulture);
        public static string ShiftType => ResourceManager.GetString("ShiftType", resourceCulture);
        public static string SelectReligion => ResourceManager.GetString("SelectReligion", resourceCulture);
        public static string SelectNationality => ResourceManager.GetString("SelectNationality", resourceCulture);
        public static string SelectShiftType => ResourceManager.GetString("SelectShiftType", resourceCulture);
        public static string HolidayDetails => ResourceManager.GetString("HolidayDetails", resourceCulture);
        public static string HolidayDetail_GridTitle => ResourceManager.GetString("HolidayDetail_GridTitle", resourceCulture);
        public static string HolidayDetail_Add => ResourceManager.GetString("HolidayDetail_Add", resourceCulture);
        public static string FromDate => ResourceManager.GetString("FromDate", resourceCulture);
        public static string ToDate => ResourceManager.GetString("ToDate", resourceCulture);
        public static string RequiredField => ResourceManager.GetString("RequiredField", resourceCulture);
        public static string InvalidDateRange => ResourceManager.GetString("InvalidDateRange", resourceCulture);
        public static string HolidayOverlapError => ResourceManager.GetString("HolidayOverlapError", resourceCulture);
        public static string TotalEmployees => ResourceManager.GetString("TotalEmployees", resourceCulture);
        public static string PendingRequests => ResourceManager.GetString("PendingRequests", resourceCulture);
        public static string ActiveContracts => ResourceManager.GetString("ActiveContracts", resourceCulture);
        public static string AttendanceAvg => ResourceManager.GetString("AttendanceAvg", resourceCulture);
        public static string AttendanceTrend => ResourceManager.GetString("AttendanceTrend", resourceCulture);
        public static string GenderDistribution => ResourceManager.GetString("GenderDistribution", resourceCulture);
        public static string MyLeaveBalance => ResourceManager.GetString("MyLeaveBalance", resourceCulture);
        public static string MyAttendance => ResourceManager.GetString("MyAttendance", resourceCulture);
        public static string MyAttendanceConsistency => ResourceManager.GetString("MyAttendanceConsistency", resourceCulture);
        public static string LeaveUsageBreakdown => ResourceManager.GetString("LeaveUsageBreakdown", resourceCulture);
        public static string NextHoliday => ResourceManager.GetString("NextHoliday", resourceCulture);
        public static string Last6Months => ResourceManager.GetString("Last6Months", resourceCulture);
        public static string LastYear => ResourceManager.GetString("LastYear", resourceCulture);
        public static string WelcomeUser => ResourceManager.GetString("WelcomeUser", resourceCulture);
        public static string DashboardSummary => ResourceManager.GetString("DashboardSummary", resourceCulture);
        public static string Expected => ResourceManager.GetString("Expected", resourceCulture);
        public static string Actual => ResourceManager.GetString("Actual", resourceCulture);
        public static string Days => ResourceManager.GetString("Days", resourceCulture);
        public static string AboveAvg => ResourceManager.GetString("AboveAvg", resourceCulture);
        public static string Pending => ResourceManager.GetString("Pending", resourceCulture);
        public static string EidAlAdha => ResourceManager.GetString("EidAlAdha", resourceCulture);
        public static string XDays => ResourceManager.GetString("XDays", resourceCulture);
        public static string ContractExpiryWarning => ResourceManager.GetString("ContractExpiryWarning", resourceCulture);
        public static string RequestLeave => ResourceManager.GetString("RequestLeave", resourceCulture);
        public static string Annual => ResourceManager.GetString("Annual", resourceCulture);
        public static string Sick => ResourceManager.GetString("Sick", resourceCulture);
        public static string Other => ResourceManager.GetString("Other", resourceCulture);
        public static string Used => ResourceManager.GetString("Used", resourceCulture);
        public static string ContractReminder => ResourceManager.GetString("ContractReminder", resourceCulture);
        public static string Workflow => ResourceManager.GetString("Workflow", resourceCulture);
        public static string Export => ResourceManager.GetString("Export", resourceCulture);
        public static string ExportGridDataOnly => ResourceManager.GetString("ExportGridDataOnly", resourceCulture);
    }
}

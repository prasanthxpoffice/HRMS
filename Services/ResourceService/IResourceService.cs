namespace HRMS.Services;

public interface IResourceService
{
    string GetString(string key);
    string GetString(string key, params object[] args);

    // Strongly-Typed Properties
    string Gender_Title => GetString("Gender_Title");
    string Gender_Description => GetString("Gender_Description");
    string Gender_GridTitle => GetString("Gender_GridTitle");
    string Gender_Add => GetString("Gender_Add");
    
    string Id => GetString("Id");
    string NameEn => GetString("NameEn");
    string NameAr => GetString("NameAr");
    string CreatedDate => GetString("CreatedDate");
    string Actions => GetString("Actions");
    
    string Search => GetString("Search");
    string All => GetString("All");
    string Add => GetString("Add");
    string Edit => GetString("Edit");
    string Delete => GetString("Delete");
    string Save => GetString("Save");
    string Cancel => GetString("Cancel");
    
    string PageTitle_Genders => GetString("PageTitle_Genders");
    string NoRecordsFound => GetString("NoRecordsFound");
    
    string AdminDashboard => GetString("AdminDashboard");
    string WelcomeBack => GetString("WelcomeBack");
    string SystemStatus => GetString("SystemStatus");
    string DatabaseConnected => GetString("DatabaseConnected");
    string AuthenticationActive => GetString("AuthenticationActive");
    string ManageGenders => GetString("ManageGenders");
    string Settings => GetString("Settings");
    string SystemDashboard => GetString("SystemDashboard");
    string DashboardDescription => GetString("DashboardDescription");
    
    string BulkDeleteSuccess => GetString("BulkDeleteSuccess");
    string BulkDeleteError => GetString("BulkDeleteError");
    string DeleteSelected => GetString("DeleteSelected");
}

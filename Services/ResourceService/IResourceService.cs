namespace HRMS.Services;

public interface IResourceService
{
    string GetString(string key);
    string GetString(string key, params object[] args);
}

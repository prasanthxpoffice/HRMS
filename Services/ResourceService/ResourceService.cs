using System.Globalization;
using System.Resources;

namespace HRMS.Services;

public class ResourceService : IResourceService
{
    private readonly LanguageService _languageService;
    private readonly ResourceManager _resourceManager;

    public ResourceService(LanguageService languageService)
    {
        _languageService = languageService;
        _resourceManager = new ResourceManager("HRMS.Resources.AppResources", typeof(ResourceService).Assembly);
    }

    public string GetString(string key)
    {
        var culture = new CultureInfo(_languageService.CurrentLanguage);
        return _resourceManager.GetString(key, culture) ?? key;
    }

    public string GetString(string key, params object[] args)
    {
        var format = GetString(key);
        return args.Length > 0 ? string.Format(format, args) : format;
    }
}

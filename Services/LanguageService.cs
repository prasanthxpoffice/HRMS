using System.Globalization;
using HRMS.Resources;

namespace HRMS.Services
{
    public class LanguageService
    {
        public string CurrentLanguage => CultureInfo.CurrentCulture.Name;
        public bool IsRtl => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;

        public void SyncResources()
        {
            AppResources.Culture = CultureInfo.CurrentCulture;
        }
    }
}

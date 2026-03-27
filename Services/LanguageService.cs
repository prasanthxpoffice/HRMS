namespace HRMS.Services
{
    public class LanguageService
    {
        public string CurrentLanguage => System.Globalization.CultureInfo.CurrentCulture.Name;
        public bool IsRtl => System.Globalization.CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;
    }
}

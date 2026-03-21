namespace HRMS.Services
{
    public class LanguageService
    {
        public string CurrentLanguage { get; private set; } = "en-US";
        public bool IsRtl => CurrentLanguage == "ar-AE";

        public event Action? OnLanguageChanged;

        public void SetLanguage(string language)
        {
            if (CurrentLanguage != language)
            {
                CurrentLanguage = language;
                
                var culture = new System.Globalization.CultureInfo(language);
                System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
                System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

                OnLanguageChanged?.Invoke();
            }
        }
    }
}

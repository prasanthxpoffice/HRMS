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
                OnLanguageChanged?.Invoke();
            }
        }
    }
}

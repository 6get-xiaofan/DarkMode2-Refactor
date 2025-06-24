namespace DarkMode.Ui.Resources;

public class SupportedLanguage
{
    public string LanguageName { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = string.Empty;

    public override string ToString() => LanguageName;
}


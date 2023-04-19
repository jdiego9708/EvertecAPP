namespace Evertec.APP.Services
{
    public static class TranslationService
    {
        private static readonly Dictionary<string, string> SpanishToEnglishDictionary = new Dictionary<string, string>
        {
            {"Configuracion", "Configuration"},
            {"Tema de la aplicación", "Theme of Application"},
            {"Claro", "Light"},
            {"Oscuro", "Dark"},
            {"Cambiar idioma", "Change language"},
            {"Español", "Spanish"},
            {"Ingles", "English"},
            {"Último ajuste", "Last changed"},
        };
        public static string TranslateFromSpanishToEnglish(this string spanishText)
        {
            if (SpanishToEnglishDictionary.TryGetValue(spanishText, out var englishText))
            {
                return englishText;
            }

            return spanishText;
        }
        public static string TranslateFromEnglishToSpanish(this string englishText)
        {
            var englishToSpanishDictionary = SpanishToEnglishDictionary
                .ToDictionary(pair => pair.Value, pair => pair.Key);

            if (englishToSpanishDictionary.TryGetValue(englishText, out var spanishText))
            {
                return spanishText;
            }

            return englishText;
        }
    }
}

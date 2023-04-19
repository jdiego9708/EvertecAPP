using Evertec.APP.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using SISEvertec.APP.Services.Interfaces;
using SISEvertec.APP.ViewModels.VMMain;
using SISEvertec.APP.Views.ViewsMessages;
using SISEvertec.Entities.Helpers.Interfaces;
using SISEvertec.Entities.Models;
using SISEvertec.Entities.ModelsBinding.ModelsConfiguration.ConfigurationSISEvertec.Translate;

namespace SISEvertec.APP.ViewModels.VMHome
{
    public class HomeViewModel : BaseViewModel
    {
        #region CONSTRUCTOR
        public IConfiguration_evertecService Configuration_evertecService { get; set; }
        public IRestHelper RestHelper { get; set; }
        public MessagesService MessagesService { get; set; }
        public HomeViewModel(IRestHelper IRestHelper,
            MessagesService MessagesService,
            IConfiguration_evertecService Configuration_evertecService,
            IConfiguration Configuration)
        {
            this.RestHelper = IRestHelper;
            this.MessagesService = MessagesService;
            this.Configuration_evertecService = Configuration_evertecService;
        }
        #endregion

        #region METHODS      
        public async Task LoadLastChanged()
        {
            try
            {
                var result = await this.Configuration_evertecService.GetAll();

                Configuration_evertec lastConfig = result.OrderByDescending(x => x.Id).FirstOrDefault();

                this.LastConfig = lastConfig;
            }
            catch (Exception ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
        }
        private async Task InsertChanged(string theme, string languaje)
        {
            try
            {
                Configuration_evertec changed = new()
                {
                    Theme_default = theme,
                    Languaje_default = languaje,
                    Last_changed = DateTime.Now,
                };

                await this.Configuration_evertecService.Add(changed);

                await this.LoadLastChanged();
            }
            catch (Exception ex)
            {
                await this.MessagesService.ShowDialogError(ex.Message);
            }
        }
        private void ChangeLanguajeSpanishToEnglish()
        {
            this.ConfigurationTextView = TranslationService.TranslateFromSpanishToEnglish("Configuracion");
            this.ThemeApplicationTextView = TranslationService.TranslateFromSpanishToEnglish("Tema de la aplicación");
            this.ThemeLightTextView = TranslationService.TranslateFromSpanishToEnglish("Claro");
            this.ThemeDarkTextView = TranslationService.TranslateFromSpanishToEnglish("Oscuro");
            this.ChangeLanguage = TranslationService.TranslateFromSpanishToEnglish("Cambiar idioma");
            this.Spanish = TranslationService.TranslateFromSpanishToEnglish("Español");
            this.English = TranslationService.TranslateFromSpanishToEnglish("Ingles");

            if (this.LastConfig != null)
                this.LastChangedTextView = $"{TranslationService.TranslateFromSpanishToEnglish("Último ajuste")} {this.LastConfig.Last_changed:dd MMMM, yyyy} {this.LastConfig.Last_changed:HH:mm tt}";
        }
        private void ChangeLanguajeEnglishToSpanish()
        {
            this.ConfigurationTextView = TranslationService.TranslateFromEnglishToSpanish("Configuration");
            this.ThemeApplicationTextView = TranslationService.TranslateFromEnglishToSpanish("Theme of Application");
            this.ThemeLightTextView = TranslationService.TranslateFromEnglishToSpanish("Light");
            this.ThemeDarkTextView = TranslationService.TranslateFromEnglishToSpanish("Dark");
            this.ChangeLanguage = TranslationService.TranslateFromEnglishToSpanish("Change language");
            this.Spanish = TranslationService.TranslateFromEnglishToSpanish("Spanish");
            this.English = TranslationService.TranslateFromEnglishToSpanish("English");

            if (this.LastConfig != null)
                this.LastChangedTextView = $"{TranslationService.TranslateFromEnglishToSpanish("Last changed")} {this.LastConfig.Last_changed:dd MMMM, yyyy} {this.LastConfig.Last_changed:HH:mm tt}";
        }
        #endregion

        #region PROPERTIES
        private bool _isDarkMode;
        private bool _isSpanish;
        private string _imageDefault;
        private Configuration_evertec _lastConfig;
        public string ImageDefault
        {
            get => _imageDefault;
            set
            {
                _imageDefault = value;
                OnPropertyChanged(nameof(ImageDefault));
            }
        }
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;

                if (value)
                {
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    Task.Run(async () => await this.InsertChanged("DarkMode", this.IsSpanish ? "Spanish" : "English"));
                }
                else
                {
                    Application.Current.UserAppTheme = AppTheme.Light;
                    Task.Run(async () => await this.InsertChanged("LightMode", this.IsSpanish ? "Spanish" : "English"));
                }

                OnPropertyChanged(nameof(IsDarkMode));
            }
        }
        public bool IsSpanish
        {
            get => _isSpanish;
            set
            {
                _isSpanish = value;

                if (value)
                {
                    this.ChangeLanguajeEnglishToSpanish();
                    Task.Run(async () => await this.InsertChanged(this.IsDarkMode ? "DarkMode" : "LightMode", this.IsSpanish ? "Spanish" : "English"));
                }
                else
                {
                    this.ChangeLanguajeSpanishToEnglish();
                    Task.Run(async () => await this.InsertChanged(this.IsDarkMode ? "DarkMode" : "LightMode", this.IsSpanish ? "Spanish" : "English"));
                }

                OnPropertyChanged(nameof(IsSpanish));
            }
        }
        public Configuration_evertec LastConfig
        {
            get => _lastConfig;
            set
            {
                _lastConfig = value;
                OnPropertyChanged(nameof(LastConfig));
            }
        }
        #endregion

        #region PROPERTIES TRANSLATE
        private string _configurationTextView;
        private string _themeApplicationTextView;
        private string _themeLightTextView;
        private string _themeDarkTextView;
        private string _changeLanguage;
        private string _spanish;
        private string _english;
        private string _lastChangedTextView;
        public string LastChangedTextView
        {
            get => _lastChangedTextView;
            set
            {
                _lastChangedTextView = value;
                OnPropertyChanged(nameof(LastChangedTextView));
            }
        }
        public string English
        {
            get => _english;
            set
            {
                _english = value;
                OnPropertyChanged(nameof(English));
            }
        }
        public string Spanish
        {
            get => _spanish;
            set
            {
                _spanish = value;
                OnPropertyChanged(nameof(Spanish));
            }
        }
        public string ChangeLanguage
        {
            get => _changeLanguage;
            set
            {
                _changeLanguage = value;
                OnPropertyChanged(nameof(ChangeLanguage));
            }
        }
        public string ThemeDarkTextView
        {
            get => _themeDarkTextView;
            set
            {
                _themeDarkTextView = value;
                OnPropertyChanged(nameof(ThemeDarkTextView));
            }
        }
        public string ThemeLightTextView
        {
            get => _themeLightTextView;
            set
            {
                _themeLightTextView = value;
                OnPropertyChanged(nameof(ThemeLightTextView));
            }
        }
        public string ConfigurationTextView
        {
            get => _configurationTextView;
            set
            {
                _configurationTextView = value;
                OnPropertyChanged(nameof(ConfigurationTextView));
            }
        }
        public string ThemeApplicationTextView
        {
            get => _themeApplicationTextView;
            set
            {
                _themeApplicationTextView = value;
                OnPropertyChanged(nameof(ThemeApplicationTextView));
            }
        }
        #endregion
    }
}

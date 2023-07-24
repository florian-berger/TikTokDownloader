using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TikTokLoaderMAUI.Base;
using TikTokLoaderMAUI.i18n;
using TikTokLoaderMAUI.Utils;

namespace TikTokLoaderMAUI.ViewModel
{
    public partial class SettingsViewModel : ViewModelBase
    {
        #region Properties

        [ObservableProperty] 
        private bool _analyzeClipboardOnLoad;

        [ObservableProperty]
        private string _currentThemeName = string.Empty;

        [ObservableProperty]
        private string _currentLanguageName = string.Empty;

        #endregion Properties

        #region Constructor

        public SettingsViewModel()
        {
            _analyzeClipboardOnLoad = AppSettings.AnalyzeClipboardOnLoad;
            UpdateThemeDisplayName();
            UpdateLanguageDisplayName();
        }

        #endregion Constructor

        #region Commands

        [RelayCommand]
        public async Task ChangeTheme()
        {
            var possibilities = new[]
            {
                new { Id = (int)AppTheme.Unspecified, Theme = AppTheme.Unspecified, Name = SettingsResource.ThemeSystem },
                new { Id = (int)AppTheme.Light, Theme = AppTheme.Light, Name = SettingsResource.ThemeLight },
                new { Id = (int)AppTheme.Dark, Theme = AppTheme.Dark, Name = SettingsResource.ThemeDark }
            };

            var selectedThemeName = await Shell.Current.DisplayActionSheet(SettingsResource.SelectTheme, GlobalResource.Cancel, null,
                possibilities.Select(p => p.Name).ToArray());

            if (selectedThemeName == null)
            {
                // When the selection was canceled, don't change anything
                return;
            }

            var selectedTheme = possibilities.FirstOrDefault(p => p.Name == selectedThemeName);
            if (selectedTheme == null)
            {
                return;
            }

            AppSettings.UsedTheme = selectedTheme.Id;
            App.SetTheme(selectedTheme.Theme);

            UpdateThemeDisplayName();
        }

        [RelayCommand]
        public async Task ChangeLanguage()
        {
            var possibilities = new[]
            {
                new { Id = "", Name = SettingsResource.LanguageSystem },
                new { Id = "en", Name = SettingsResource.LanguageEnglish },
                new { Id = "de", Name = SettingsResource.LanguageGerman }
            };

            var selectedLanguageName = await Shell.Current.DisplayActionSheet(SettingsResource.SelectLanguage, GlobalResource.Cancel, null,
                possibilities.Select(p => p.Name).ToArray());

            if (selectedLanguageName == null)
            {
                // When the selection was canceled, don't change anything
                return;
            }

            var selectedLanguage = possibilities.FirstOrDefault(p => p.Name == selectedLanguageName);
            if (selectedLanguage == null)
            {
                return;
            }

            AppSettings.AppLanguage = selectedLanguage.Id;
            App.SetLanguage(selectedLanguage.Id);

            UpdateLanguageDisplayName();

            // Restart the application to apply the language change
            if (Application.Current != null)
            {
                var newShell = new AppShell();
                Application.Current.MainPage = newShell;

                await Shell.Current.GoToAsync("//SettingsPage", false);
            }
        }

        [RelayCommand]
        public async Task ChangeApiEndpoint()
        {
            await Shell.Current.DisplayAlert("Not implemented", "The API endpoint cannot be changed yet.", GlobalResource.Ok);
        }

        [RelayCommand]
        public void ChangeAnalyzeClipboard()
        {
            AnalyzeClipboardOnLoad = AppSettings.AnalyzeClipboardOnLoad = !AppSettings.AnalyzeClipboardOnLoad;
        }

        #endregion Commands

        #region Private methods

        private void UpdateThemeDisplayName()
        {
            CurrentThemeName = AppSettings.UsedTheme switch
            {
                (int)AppTheme.Unspecified => SettingsResource.ThemeSystem,
                (int)AppTheme.Light => SettingsResource.ThemeLight,
                (int)AppTheme.Dark => SettingsResource.ThemeDark,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void UpdateLanguageDisplayName()
        {
            CurrentLanguageName = AppSettings.AppLanguage switch
            {
                "" => SettingsResource.LanguageSystem,
                "en" => SettingsResource.LanguageEnglish,
                "de" => SettingsResource.LanguageGerman,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion Private methods
    }
}

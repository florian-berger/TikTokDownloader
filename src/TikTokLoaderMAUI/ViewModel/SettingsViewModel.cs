using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TikTokLoaderMAUI.Base;
using TikTokLoaderMAUI.Utils;

namespace TikTokLoaderMAUI.ViewModel
{
    public partial class SettingsViewModel : ViewModelBase
    {
        #region Properties

        [ObservableProperty] 
        private bool _analyzeClipboardOnLoad;

        [ObservableProperty]
        private string _currentThemeName;

        #endregion Properties

        #region Constructor

        public SettingsViewModel()
        {
            _analyzeClipboardOnLoad = AppSettings.AnalyzeClipboardOnLoad;

            UpdateThemeDisplayName();
        }

        #endregion Constructor

        #region Commands

        [RelayCommand]
        public async Task ChangeTheme()
        {
            var possibilities = new[]
            {
                new { Id = (int)AppTheme.Light, Theme = AppTheme.Light, Name = "Light" },
                new { Id = (int)AppTheme.Dark, Theme = AppTheme.Dark, Name = "Dark" },
                new { Id = (int)AppTheme.Unspecified, Theme = AppTheme.Unspecified, Name = "System" }
            };

            var selectedThemeName = await Shell.Current.DisplayActionSheet("Select theme", "Cancel", null,
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
            await Shell.Current.DisplayAlert("Not implemented", "The language cannot be changed yet.", "OK");
        }

        [RelayCommand]
        public async Task ChangeApiEndpoint()
        {
            await Shell.Current.DisplayAlert("Not implemented", "The API endpoint cannot be changed yet.", "OK");
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
                (int)AppTheme.Unspecified => "System",
                (int)AppTheme.Light => "Light",
                (int)AppTheme.Dark => "Dark",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion Private methods
    }
}

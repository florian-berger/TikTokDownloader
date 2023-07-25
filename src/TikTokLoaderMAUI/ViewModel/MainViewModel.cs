using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TikTokLoader.Logic;
using TikTokLoaderMAUI.Base;
using TikTokLoader.Logic.Data;
using TikTokLoaderMAUI.i18n;
using TikTokLoaderMAUI.Utils;

namespace TikTokLoaderMAUI.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        ///     Entered URI for analyzing and TikTok download
        /// </summary>
        [ObservableProperty]
        private string _downloadUri;

        /// <summary>
        ///     Result of the analyzed URI
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasDownloadDetails))]
        [NotifyCanExecuteChangedFor(nameof(DownloadWithWatermarkCommand))]
        [NotifyCanExecuteChangedFor(nameof(DownloadWithoutWatermarkCommand))]
        [NotifyCanExecuteChangedFor(nameof(DownloadMusicCommand))]
        private DownloadDetails? _data;

        /// <summary>
        ///     Information if the app is currently analyzing a URI
        /// </summary>
        [ObservableProperty]
        private bool _isAnalyzing;

        /// <summary>
        ///     Information if there are download details available
        /// </summary>
        public bool HasDownloadDetails => Data != null;

        #endregion Properties

        #region Constructor

        public MainViewModel()
        {
            _downloadUri = string.Empty;
        }

        #endregion Constructor

        #region Commands

        /// <summary>
        ///     Command that starts the URI analysis
        /// </summary>
        [RelayCommand]
        public async void AnalyzeUri()
        {
            Data = null;
            GridTappedCommand.Execute(null);

            IsAnalyzing = true;
            try
            {
                Data = await Analyzer.AnalyzeUri(DownloadUri);
            }
            catch (Exception ex)
            {
                await ExceptionHelper.DisplayExceptionMessage(ex);
            }
            finally
            {
                IsAnalyzing = false;
            }
        }

        [RelayCommand]
        public async Task AnalyzeClipboard()
        {
            if (!AppSettings.AnalyzeClipboardOnLoad)
            {
                return;
            }

            var clipboardText = await Clipboard.GetTextAsync();
            if (string.IsNullOrWhiteSpace(clipboardText))
            {
                return;
            }

            if (clipboardText.Equals(DownloadUri, StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            if (clipboardText.Contains("tiktok.com", StringComparison.InvariantCultureIgnoreCase))
            {
                DownloadUri = clipboardText;
                AnalyzeUriCommand.Execute(null);
            }
        }

        /// <summary>
        ///     Downloads the current TikTok video without watermark
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanDownloadWithoutWatermark))]
        public async Task DownloadWithoutWatermark()
        {
            if (!HasDownloadDetails)
            {
                return;
            }

            var downloadUrl = Data?.NoWatermarkVideoUri;
            if (string.IsNullOrWhiteSpace(downloadUrl))
            {
                return;
            }

            await DownloadMedia(downloadUrl, $"{Data?.Id}.mp4");
        }

        /// <summary>
        ///     Downloads the current TikTok video with watermark
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanDownloadWithWatermark))]
        public async Task DownloadWithWatermark()
        {
            if (!HasDownloadDetails)
            {
                return;
            }

            var downloadUrl = Data?.WatermarkVideoUri;
            if (string.IsNullOrWhiteSpace(downloadUrl))
            {
                return;
            }

            await DownloadMedia(downloadUrl, $"{Data?.Id}_wm.mp4");
        }

        /// <summary>
        ///     Downloads the sound of the current TikTok video
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanDownloadMusic))]
        public async Task DownloadMusic()
        {
            if (!HasDownloadDetails)
            {
                return;
            }

            var downloadUrl = Data?.WatermarkVideoUri;
            if (string.IsNullOrWhiteSpace(downloadUrl))
            {
                return;
            }

            await DownloadMedia(downloadUrl, $"{Data?.Id}.mp3");
        }

        /// <summary>
        ///     Triggered when the user tapped outside of the box to close the clipboard
        /// </summary>
        [RelayCommand]
        public void GridTapped()
        {
            // Fix for the fucking fucked Android MAUI issue that the keyboard never closes...!
#if ANDROID
            var systemService = MauiApplication.Current.GetSystemService(Android.Content.Context.InputMethodService);
            if (systemService is Android.Views.InputMethods.InputMethodManager imm)
            {
                //this stuff came from here: https://www.syncfusion.com/kb/12559/how-to-hide-the-keyboard-when-scrolling-in-xamarin-forms-listview-sflistview
                var activity = Platform.CurrentActivity;
                var wToken = activity?.CurrentFocus?.WindowToken;
                imm.HideSoftInputFromWindow(wToken, 0);
            }
#endif
        }

        #endregion Commands

        #region Private methods

        private async Task DownloadMedia(string downloadUri, string fileName)
        {
            bool successfully;
            try
            {
                successfully = await DownloadHelper.DownloadMediaAsync(downloadUri, fileName);
            }
            catch
            {
                successfully = false;
            }

            await Toast.Make(successfully ? DownloadResource.DownloadSucceeded : DownloadResource.DownloadFailed).Show();
        }

        private bool CanDownloadWithoutWatermark()
        {
            return !string.IsNullOrWhiteSpace(Data?.NoWatermarkVideoUri);
        }

        private bool CanDownloadWithWatermark()
        {
            return !string.IsNullOrWhiteSpace(Data?.WatermarkVideoUri);
        }

        private bool CanDownloadMusic()
        {
            return !string.IsNullOrWhiteSpace(Data?.MusicUri);
        }

        #endregion Private methods
    }
}

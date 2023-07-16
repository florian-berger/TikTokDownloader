using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TikTokLoaderMAUI.Base;
using TikTokLoaderMAUI.Classes;

namespace TikTokLoaderMAUI.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        #region Properties

        [ObservableProperty]
        private string _downloadUri;

        [ObservableProperty]
        private bool _isDownloadRunning;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasDownloadDetails))]
        [NotifyCanExecuteChangedFor(nameof(DownloadWithWatermarkCommand))]
        [NotifyCanExecuteChangedFor(nameof(DownloadWithoutWatermarkCommand))]
        [NotifyCanExecuteChangedFor(nameof(DownloadMusicCommand))]
        private DownloadDetails? _data;

        [ObservableProperty]
        private bool _isAnalyzing;

        public bool HasDownloadDetails => Data != null;

        #endregion Properties

        #region Constructor

        public MainViewModel()
        {
        }

        #endregion Constructor

        #region Commands

        [RelayCommand]
        public async void AnalyzeUri()
        {
            GridTappedCommand.Execute(null);

            if (string.IsNullOrWhiteSpace(DownloadUri))
            {
                await Shell.Current.DisplayAlert("No URL provided", "You haven't provided any URL.", "OK");
                return;
            }

            IsAnalyzing = true;
            try
            {
                Data = await TikTokLoader.AnalyzeUri(DownloadUri);
            }
            catch (Exception ex)
            {
                Data = null!;

                await Shell.Current.DisplayAlert("ERROR", ex.Message, "OK");
            }
            finally
            {
                IsAnalyzing = false;
            }
        }

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

            bool successfully;
            var filename = $"{Data.Id}.mp4";
            try
            {
                successfully = await TikTokLoader.DownloadMediaAsync(downloadUrl, filename);
            }
            catch
            {
                successfully = false;
            }

            await Toast.Make($"Download {(successfully ? "finished" : "failed")}.").Show();
        }

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

            bool successfully;
            var filename = $"{Data.Id}_wm.mp4";
            try
            {
                successfully = await TikTokLoader.DownloadMediaAsync(downloadUrl, filename);
            }
            catch
            {
                successfully = false;
            }

            await Toast.Make($"Download {(successfully ? "finished" : "failed")}.").Show();
        }

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

            bool successfully;
            var filename = $"{Data.Id}.mp3";
            try
            {
                successfully  = await TikTokLoader.DownloadMediaAsync(downloadUrl, filename);
            }
            catch
            {
                successfully = false;
            }

            await Toast.Make($"Download {(successfully ? "finished" : "failed")}.").Show();
        }

        [RelayCommand]
        public void GridTapped()
        {
            // Fix for the fucking fucked Android MAUI issue that the keyboard never closes...!
#if ANDROID
            var imm = (Android.Views.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(Android.Content.Context.InputMethodService);
            if (imm != null)
            {
                //this stuff came from here:  https://www.syncfusion.com/kb/12559/how-to-hide-the-keyboard-when-scrolling-in-xamarin-forms-listview-sflistview
                var activity = Platform.CurrentActivity;
                var wToken = activity?.CurrentFocus?.WindowToken;
                imm.HideSoftInputFromWindow(wToken, 0);
            }
#endif
        }

        #endregion Commands

        #region Private methods

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

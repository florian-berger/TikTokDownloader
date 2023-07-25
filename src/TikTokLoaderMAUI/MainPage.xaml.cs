using TikTokLoaderMAUI.ViewModel;

namespace TikTokLoaderMAUI;

/// <summary>
///     Main page of the application
/// </summary>
public partial class MainPage
{
    #region Constructor

    /// <summary>
    ///     Creates an instance of the main page
    /// </summary>
    public MainPage()
	{
		InitializeComponent();
    }

    #endregion Constructor

    #region Private methods

    private void UnFocusEntryAndSend(object sender, EventArgs e)
    {
        UriEntry.Unfocus();

        if (BindingContext is MainViewModel mainViewModel)
        {
            mainViewModel.AnalyzeUriCommand.Execute(null);
        }
    }

    #endregion Private methods
}


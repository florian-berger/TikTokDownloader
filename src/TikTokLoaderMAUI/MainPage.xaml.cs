using TikTokLoaderMAUI.ViewModel;

namespace TikTokLoaderMAUI;

public partial class MainPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void UnFocusEntryAndSend(object sender, EventArgs e)
    {
        UriEntry.Unfocus();

        if (BindingContext is MainViewModel mainViewModel)
        {
            mainViewModel.AnalyzeUriCommand.Execute(null);
        }
    }
}


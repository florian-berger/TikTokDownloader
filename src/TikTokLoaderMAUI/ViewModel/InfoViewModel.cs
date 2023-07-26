using CommunityToolkit.Mvvm.Input;
using TikTokLoaderMAUI.Base;
using TikTokLoaderMAUI.BindingObjects;

namespace TikTokLoaderMAUI.ViewModel
{
    /// <summary>
    ///     ViewModel for the Info page
    /// </summary>
    public partial class InfoViewModel : ViewModelBase
    {
        #region Constants

        // ReSharper disable once InconsistentNaming
        private static readonly IReadOnlyList<ThirdPartyPackage> _packages = (new List<ThirdPartyPackage>
            {
                new()
                {
                    Name = "CommunityToolkit.Maui",
                    Author = "Microsoft",
                    Version = "5.2.0",
                    HomepageUri = "https://github.com/CommunityToolkit/Maui",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://github.com/CommunityToolkit/Maui/blob/main/LICENSE" }
                    }
                },
                new()
                {
                    Name = "CommunityToolkit.Mvvm",
                    Author = "Microsoft",
                    Version = "8.0.0",
                    HomepageUri = "https://github.com/CommunityToolkit/Maui",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://github.com/CommunityToolkit/Maui/blob/main/LICENSE" }
                    }
                },
                new()
                {
                    Name = "coverlet.collector",
                    Author = "tonerdo",
                    Version = "3.2.0",
                    HomepageUri = "https://github.com/coverlet-coverage/coverlet",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://github.com/coverlet-coverage/coverlet/blob/master/LICENSE" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Extensions.Configuration",
                    Author = "Microsoft",
                    Version = "7.0.0",
                    HomepageUri = "https://dot.net",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Extensions.DependencyInjection",
                    Author = "Microsoft",
                    Version = "7.0.0",
                    HomepageUri = "https://dot.net",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Extensions.Logging",
                    Author = "Microsoft",
                    Version = "7.0.0",
                    HomepageUri = "https://dot.net",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Extensions.Logging.Abstractions",
                    Author = "Microsoft",
                    Version = "7.0.0",
                    HomepageUri = "https://dot.net",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Extensions.Logging.Debug",
                    Author = "Microsoft",
                    Version = "7.0.0",
                    HomepageUri = "https://dot.net",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Maui.Graphics",
                    Author = "Microsoft",
                    Version = "7.0.86",
                    HomepageUri = "https://github.com/dotnet/maui",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://github.com/dotnet/maui/blob/main/LICENSE.txt" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Maui.Graphics.Win2D.WinUI.Desktop",
                    Author = "Microsoft",
                    Version = "7.0.86",
                    HomepageUri = "https://dot.net",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://github.com/dotnet/maui/blob/main/LICENSE.txt" }
                    }
                },
                new()
                {
                    Name = "Microsoft.NET.Test.Sdk",
                    Author = "Microsoft",
                    Version = "17.5.0",
                    HomepageUri = "https://github.com/microsoft/vstest/",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "Microsoft Software License Terms", "https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/17.5.0/license" }
                    }
                },
                new()
                {
                    Name = "Microsoft.Windows.SDK.BuildTools",
                    Author = "Microsoft",
                    Version = "10.0.22621.755",
                    HomepageUri = "https://aka.ms/WinSDKProjectURL",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "Microsoft Software License Terms", "https://aka.ms/WinSDKLicenseURL" }
                    }
                },
                new()
                {
                    Name = "Microsoft.WindowsAppSDK",
                    Author = "Microsoft",
                    Version = "1.2.221209.1",
                    HomepageUri = "https://github.com/microsoft/windowsappsdk",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "Microsoft Software License Terms", "https://www.nuget.org/packages/Microsoft.WindowsAppSDK/1.2.221209.1/license" }
                    }
                },
                new()
                {
                    Name = "MSTest.TestAdapter",
                    Author = "Microsoft",
                    Version = "2.2.10",
                    HomepageUri = "https://github.com/microsoft/testfx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" }
                    }
                },
                new()
                {
                    Name = "MSTest.TestFramework",
                    Author = "Microsoft",
                    Version = "2.2.10",
                    HomepageUri = "https://github.com/microsoft/testfx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" }
                    }
                },
                new()
                {
                    Name = "Xamarin.Android.Glide",
                    Author = "Microsoft",
                    Version = "4.13.2.2",
                    HomepageUri = "https://go.microsoft.com/fwlink/?linkid=865435",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Browser",
                    Author = "Microsoft",
                    Version = "1.4.0.3",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Legacy.Support.V4",
                    Author = "Microsoft",
                    Version = "1.0.0.15",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Lifecycle.LiveData",
                    Author = "Microsoft",
                    Version = "2.5.1.1",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Navigation.Common",
                    Author = "Microsoft",
                    Version = "2.5.2.1",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Navigation.Fragment",
                    Author = "Microsoft",
                    Version = "2.5.2.1",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Navigation.Runtime",
                    Author = "Microsoft",
                    Version = "2.5.2.1",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Navigation.UI",
                    Author = "Microsoft",
                    Version = "2.5.2.1",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.AndroidX.Security.SecurityCrypto",
                    Author = "Microsoft",
                    Version = "1.1.0-alpha03",
                    HomepageUri = "https://go.microsoft.com/fwlink/?linkid=2113238",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                    }
                },
                new()
                {
                    Name = "Xamarin.Google.Android.Material",
                    Author = "Microsoft",
                    Version = "1.7.0",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "Xamarin.Google.Crypto.Tink.Android",
                    Author = "Microsoft",
                    Version = "1.7.0.1",
                    HomepageUri = "https://aka.ms/androidx",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://licenses.nuget.org/MIT" },
                        { "Apache-2.0", "https://licenses.nuget.org/Apache-2.0" }
                    }
                },
                new()
                {
                    Name = "AathifMahir.Maui.MauiIcons.Material",
                    Author = "Aathif Mahir",
                    Version = "1.2.0",
                    HomepageUri = "https://github.com/AathifMahir/MauiIcons",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://github.com/AathifMahir/MauiIcons/blob/master/LICENSE" }
                    }
                },
                new()
                {
                    Name = "The49.Maui.BottomSheet",
                    Author = "The49.Maui.BottomSheet",
                    Version = "1.0.1",
                    HomepageUri = "https://github.com/the49ltd/The49.Maui.BottomSheet",
                    LicenseInfo = new Dictionary<string, string?>
                    {
                        { "MIT", "https://github.com/the49ltd/The49.Maui.BottomSheet/blob/main/LICENSE.md" }
                    }
                }
            }).OrderBy(p => p.Name).ToList().AsReadOnly();

        #endregion Constants

        #region Properties

        /// <summary>
        ///     List of all third party packages
        /// </summary>
        public IReadOnlyList<ThirdPartyPackage> UsedThirdPartyPackages => _packages;

        /// <summary>
        ///     Get the current version of the app
        /// </summary>
        public string CurrentAppVersion => AppInfo.VersionString;

        #endregion Properties

        #region Commands

        [RelayCommand]
        public async Task ShowLicenseDetails(ThirdPartyPackage? package)
        {
            if (package == null)
            {
                return;
            }

            var sheet = new LicenseDetailSheet(package);
            await sheet.ShowAsync();
        }

        #endregion Commands
    }
}

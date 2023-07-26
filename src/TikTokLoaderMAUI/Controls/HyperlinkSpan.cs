using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTokLoaderMAUI.Controls
{
    public class HyperlinkSpan : Span
    {
        #region BindableProperties

        public static readonly BindableProperty TargetAddressProperty =
            BindableProperty.Create(nameof(TargetAddress), typeof(string), typeof(Hyperlink), propertyChanged: RelevantPropertyChanged);

        #endregion BindableProperties

        #region Properties

        public string TargetAddress
        {
            get => (string)GetValue(TargetAddressProperty);
            set => SetValue(TargetAddressProperty, value);
        }

        #endregion Properties

        #region Private methods

        private static void RelevantPropertyChanged(BindableObject bindAble, object oldValue, object newValue)
        {
            (bindAble as HyperlinkSpan)?.ReConfigure();
        }

        private void ReConfigure()
        {
            GestureRecognizers.Clear();

            if (string.IsNullOrWhiteSpace(TargetAddress))
            {
                if (
                    Application.Current != null &&
                    Application.Current.Resources.TryGetValue("Gray900", out var lightTextColor) &&
                    Application.Current.Resources.TryGetValue("White", out var darkTextColor)
                )
                {
                    TextDecorations = TextDecorations.None;
                    this.SetAppThemeColor(TextColorProperty, (Color)lightTextColor, (Color)darkTextColor);
                }
            }
            else
            {
                TextDecorations = TextDecorations.Underline;
                this.SetAppThemeColor(TextColorProperty, Colors.Blue, Colors.LightBlue);

                GestureRecognizers.Add(new TapGestureRecognizer
                {
                    // ReSharper disable once AsyncVoidLambda
                    Command = new Command(async () => await Launcher.OpenAsync(TargetAddress))
                });
            }
        }

        #endregion Private methods
    }
}

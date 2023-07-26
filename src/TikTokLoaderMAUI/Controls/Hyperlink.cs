namespace TikTokLoaderMAUI.Controls
{
    internal class Hyperlink : Label
    {
        #region BindableProperties

        public static readonly BindableProperty TargetAddressProperty =
            BindableProperty.Create(nameof(TargetAddress), typeof(string), typeof(Hyperlink), propertyChanged: RelevantPropertyChanged);

        public static readonly BindableProperty DisplayTextProperty =
            BindableProperty.Create(nameof(DisplayText), typeof(string), typeof(Hyperlink), propertyChanged: RelevantPropertyChanged);

        #endregion BindableProperties

        #region Properties

        public string TargetAddress
        {
            get => (string)GetValue(TargetAddressProperty);
            set => SetValue(TargetAddressProperty, value);
        }

        public string DisplayText
        {
            get => (string) GetValue(DisplayTextProperty);
            set => SetValue(DisplayTextProperty, value);
        }

        [Obsolete("Use the DisplayText property instead!")]
        public new string? Text { get; set; }

        #endregion Properties

        #region Private methods

        private static void RelevantPropertyChanged(BindableObject bindAble, object oldValue, object newValue)
        {
            (bindAble as Hyperlink)?.ReDraw();
        }

        private void ReDraw()
        {
            GestureRecognizers.Clear();

            var formattedString = new FormattedString();
            if (string.IsNullOrWhiteSpace(TargetAddress))
            {
                formattedString.Spans.Add(new Span
                {
                    Text = DisplayText
                });
            }
            else
            {
                var formattedSpan = new Span
                {
                    Text = DisplayText,
                    TextDecorations = TextDecorations.Underline
                };
                formattedSpan.SetAppThemeColor(Span.TextColorProperty, Colors.Blue, Colors.LightBlue);
                formattedString.Spans.Add(formattedSpan);

                GestureRecognizers.Add(new TapGestureRecognizer
                {
                    // ReSharper disable once AsyncVoidLambda
                    Command = new Command(async () => await Launcher.OpenAsync(TargetAddress))
                });
            }

            FormattedText = formattedString;
        }

        #endregion Private methods
    }
}

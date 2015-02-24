namespace HttpCross.Testing.Silverlight
{
    using System;
    using System.Windows;

    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var response = Http.Post("http://localhost:38822/")
                .WithBody(new { hey = "zmey" })
                .CallFor<Object>();
        }
    }
}

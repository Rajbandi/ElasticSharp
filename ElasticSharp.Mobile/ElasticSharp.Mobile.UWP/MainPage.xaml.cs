namespace ElasticSharp.Mobile.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new ElasticSharp.Mobile.App());
        }
    }
}
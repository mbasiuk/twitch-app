using System.Windows;

namespace Twitch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource streamViewSource = ((System.Windows.Data.CollectionViewSource)(FindResource("streamViewSource")));
            var loader = new Models.Loaders.Dota2();
            var streamCollection = await loader.GetAsync();
                        
            streamViewSource.Source = streamCollection.streams;
            loader.UpdateEvery(streamCollection, Settings.StreamsRefresher.IntervalInSeconds, streamViewSource.View);            
        }

        private void OnTick()
        {
            
        }
    }
}

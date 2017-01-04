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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource streamViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("streamViewSource")));
            streamViewSource.Source = Models.Loaders.Dota2.Get().streams;
            // Load data by setting the CollectionViewSource.Source property:
            // streamViewSource.Source = [generic data source]
        }
    }
}

using System.Windows;
using System.Windows.Navigation;
using DigitsOfPi.Engine.ViewModels;

namespace DigitsOfPi.Views
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            DataContext = SPAViewModel.GetInstance();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
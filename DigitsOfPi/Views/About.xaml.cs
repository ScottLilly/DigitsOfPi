using System.Windows;
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
    }
}
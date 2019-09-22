using System.Windows;
using DigitsOfPi.Engine.ViewModels;

namespace DigitsOfPi.Views
{
    public partial class IncorrectDigitEntered : Window
    {
        public IncorrectDigitEntered()
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
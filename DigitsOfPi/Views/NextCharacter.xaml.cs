using System.Windows;
using DigitsOfPi.Engine.ViewModels;

namespace DigitsOfPi.Views
{
    public partial class NextCharacter : Window
    {
        public NextCharacter()
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
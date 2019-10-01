using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DigitsOfPi.Engine.ViewModels;
using DigitsOfPi.Views;

namespace DigitsOfPi
{
    public partial class MainWindow : Window
    {
        private SPAViewModel VM => DataContext as SPAViewModel;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = SPAViewModel.GetInstance();

            VM.PropertyChanged += OnPropertyChanged;
            VM.IncorrectDigitEntered += OnIncorrectDigitEntered;
        }

        private void Restart_OnClick(object sender, RoutedEventArgs e)
        {
            VM.ClearEnteredCharacters();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PacedAudio_OnClick(object sender, RoutedEventArgs e)
        {
            PacedAudio pacedAudio = new PacedAudio();
            pacedAudio.Owner = this;
            pacedAudio.ShowDialog();
        }

        private void HelpAbout_OnClick(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Owner = this;
            about.ShowDialog();
        }

        private void DisplayHint_OnClick(object sender, RoutedEventArgs e)
        {
            NextCharacter nextCharacter = new NextCharacter();
            nextCharacter.Owner = this;
            nextCharacter.ShowDialog();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(VM.CharactersEntered)))
            {
                CharactersEnteredScrollViewer.ScrollToEnd();
            }
        }

        private void OnIncorrectDigitEntered(object sender, string e)
        {
            IncorrectDigitEntered incorrectDigitEntered = new IncorrectDigitEntered();
            incorrectDigitEntered.Owner = this;
            incorrectDigitEntered.ShowDialog();
        }

        private void KeyPressed_OnKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                VM.AddCharacter(e.Key.ToString().Substring(1, 1));
            }
            else if(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                VM.AddCharacter(e.Key.ToString().Substring(6, 1));
            }
            else if(e.Key.Equals(Key.OemPeriod) || e.Key.Equals(Key.Decimal))
            {
                VM.AddCharacter(".");
            }
            else if(e.Key.Equals(Key.H))
            {
                DisplayHint_OnClick(this, new RoutedEventArgs());
            }
        }
    }
}
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DigitsOfPi.Engine.ViewModels;

namespace DigitsOfPi.Views
{
    public partial class PacedAudio : Window
    {
        private AudioTrainerViewModel VM => DataContext as AudioTrainerViewModel;
        private CancellationTokenSource _cancellationTokenSource;

        public PacedAudio()
        {
            InitializeComponent();

            DataContext = new AudioTrainerViewModel();

            Closing += OnClosing;
        }

        private void OnlyAllowNumericDigits_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !"0123456789".Contains(e.Text);
        }

        private async void StartReadingDigits_OnClick(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await VM.ReadDigits(_cancellationTokenSource.Token);
            }
            catch(OperationCanceledException)
            {
                // Ignore this exception.
                // It is automatically thrown when the user cancels reading digits
            }
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
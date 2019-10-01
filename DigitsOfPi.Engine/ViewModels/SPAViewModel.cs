using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DigitsOfPi.Engine.Helpers;
using Prism.Commands;

namespace DigitsOfPi.Engine.ViewModels
{
    public class SPAViewModel : INotifyPropertyChanged
    {
        private static readonly SPAViewModel s_spaViewModel = new SPAViewModel();

        #region Properties and backing variables

        private string _charactersEntered = "";

        public string CharactersEntered
        {
            get => _charactersEntered;
            private set
            {
                if (value == _digitsOfPi.Substring(0, value.Length))
                {
                    // User entered the correct value
                    _charactersEntered = value;

                    OnPropertyChanged();

                    NumberOfCorrectDigits = CharactersEntered.Count(char.IsDigit);
                }
                else
                {
                    OnIncorrectDigitEntered();
                }
            }
        }

        private string _lastCharacterEntered;

        public string LastCharacterEntered
        {
            get => _lastCharacterEntered;
            set
            {
                _lastCharacterEntered = value;
                OnPropertyChanged();
            }
        }

        private int _numberOfCorrectDigits;

        public int NumberOfCorrectDigits
        {
            get => _numberOfCorrectDigits;
            private set
            {
                _numberOfCorrectDigits = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public DelegateCommand<string> AddCharacterCommand =>
            _addCharacterCommand ?? (_addCharacterCommand = new DelegateCommand<string>(AddCharacter));

        public string AppVersion =>
            $"Version {Assembly.GetEntryAssembly()?.GetName().Version.ToFormattedVersion()}";

        public string NextCharacter =>
            _digitsOfPi.Substring(CharactersEntered.Length, 1);

        private readonly string _digitsOfPi;

        private DelegateCommand<string> _addCharacterCommand;

        private SPAViewModel()
        {
            _digitsOfPi = File.ReadAllText(@".\DigitsOfPi.txt");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<string> IncorrectDigitEntered;

        public static SPAViewModel GetInstance()
        {
            return s_spaViewModel;
        }

        public void ClearEnteredCharacters()
        {
            CharactersEntered = "";
        }

        public void AddCharacter(string character)
        {
            // Don't allow multiple entry of decimal points.
            if(character.Equals(".") && CharactersEntered.Contains("."))
            {
                return;
            }

            LastCharacterEntered = character;


            CharactersEntered += character;
        }

        public string GetCharacter(int index)
        {
            string character = _digitsOfPi.Substring(index, 1);

            return character == "." ? "point" : character;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnIncorrectDigitEntered()
        {
            IncorrectDigitEntered?.Invoke(this, LastCharacterEntered);
        }
    }
}
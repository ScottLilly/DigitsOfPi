using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;

namespace DigitsOfPi.Engine.ViewModels
{
    public class AudioTrainerViewModel
    {
        private readonly SPAViewModel _spaViewModel = SPAViewModel.GetInstance();
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();

        private Volume _currentVolume = Volume.Normal;
        private bool _continueSpeaking = true;

        public ObservableCollection<InstalledVoice> AvailableVoices { get; } =
            new ObservableCollection<InstalledVoice>();

        public InstalledVoice SelectedVoice { get; set; }
        public int DigitsToRead { get; set; } = 12;
        public int SpeechRate { get; set; } = 2;

        public AudioTrainerViewModel()
        {
            AvailableVoices.AddRange(_speechSynthesizer.GetInstalledVoices());
            SelectedVoice = AvailableVoices.FirstOrDefault();
        }

        public async Task ReadDigits(CancellationToken cancellationToken)
        {
            SetupVoice();

            while(_continueSpeaking)
            {
                _speechSynthesizer.Rate = 1;

                _speechSynthesizer.SpeakAsync("You have an excellent memory");
                await Task.Delay(2000, cancellationToken);

                _speechSynthesizer.SpeakAsync("You memorize numbers easily");
                await Task.Delay(4000, cancellationToken);

                SetupVoice();

                _speechSynthesizer.SpeakAsync("3");
                _speechSynthesizer.SpeakAsync("point");

                for(int i = 2; i < DigitsToRead + 2; i += 3)
                {
                    _speechSynthesizer.Volume = (int)_currentVolume;

                    _speechSynthesizer.SpeakAsync(_spaViewModel.GetCharacter(i));
                    _speechSynthesizer.SpeakAsync(_spaViewModel.GetCharacter(i + 1));
                    _speechSynthesizer.SpeakAsync(_spaViewModel.GetCharacter(i + 2));

                    await Task.Delay(4000, cancellationToken);

                    CycleToNextVolume();
                }

                await Task.Delay(4000, cancellationToken);
            }
        }

        private void SetupVoice()
        {
            _speechSynthesizer.SelectVoice(SelectedVoice.VoiceInfo.Name);
            _speechSynthesizer.Rate = SpeechRate;
        }

        private void CycleToNextVolume()
        {
            switch(_currentVolume)
            {
                case Volume.Soft:
                    _currentVolume = Volume.Loud;
                    break;
                case Volume.Normal:
                    _currentVolume = Volume.Soft;
                    break;
                case Volume.Loud:
                    _currentVolume = Volume.Normal;
                    break;
            }
        }
    }
}
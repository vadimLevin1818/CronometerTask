using CronometerTask.Domain;
using CronometerTask.UI.Services;
using CronoTask.UI.Common;
using CronoTask.UI.ViewModel;
using System.Windows.Input;

namespace CronometerTask.UI.Cronometer
{
    public class CronometerVm : ViewModelBase
    {
        private string _seconds;
        private string _minutes;
        private string _hours;
        private Domain.Cronometer.ICronometer _cronometer;
        private readonly ICommand? _startCommand;
        private readonly ICommand? _pauseCommand;
        private readonly ICommand? _stopCommand;

        public CronometerVm()
        {
            SetInitialClockParameters();
            _cronometer = Domain.Cronometer.Cronometer.CreateCronometer();
            var container = new CronometerService(_cronometer);

            _startCommand = new RelayCommand((param) => {
                container.Start();
                OnPropertyChanged(nameof(CanStart));
                OnPropertyChanged(nameof(CanStop));
                OnPropertyChanged(nameof(CanReset));
            }, param => CanStart);

            _pauseCommand = new RelayCommand((param) => 
            {
                container.Pause();
                OnPropertyChanged(nameof(CanStart));
                OnPropertyChanged(nameof(CanStop));
                OnPropertyChanged(nameof(CanReset));
            }, param => CanStop);

            _stopCommand = new RelayCommand((param) => 
            {
                container.Stop();
                OnPropertyChanged(nameof(CanStart));
                OnPropertyChanged(nameof(CanStop));
                OnPropertyChanged(nameof(CanReset));
            }, param => CanReset);

            _cronometer.UnitOfTimeElapsed += (sender, args) => {
                Seconds = args.CronometerTime.Seconds < 10 ? $"0{args.CronometerTime.Seconds}" : args.CronometerTime.Seconds.ToString();
                Minutes = args.CronometerTime.Minutes < 10 ? $"0{args.CronometerTime.Minutes}" : args.CronometerTime.Minutes.ToString();
                Hours = args.CronometerTime.Hours < 10 ? $"0{args.CronometerTime.Hours}" : args.CronometerTime.Hours.ToString();
            };

        }

        private void SetInitialClockParameters()
        {
            Seconds = "00";
            Minutes = "00";
            Hours = "00";
    }

        public ICommand? StartCommand => _startCommand;
        public ICommand? PauseCommand => _pauseCommand;
        public ICommand? StopCommand => _stopCommand;
        //{
        //    get
        //    {
        //        return _startCommand;
        //          //?? (_startCommand = new RelayCommand(
        //          //  async () =>
        //          //  {
        //          //      await Refresh();
        //          //  }));
        //    }
        //}

        public string Seconds
        {
            get => _seconds;
            private set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public string Minutes
        {
            get => _minutes;
            private set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        public string Hours
        {
            get => _hours;
            private set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        public bool CanStart => !_cronometer.IsRunning;

        public bool CanStop => _cronometer.IsRunning;

        public bool CanReset => _cronometer.IsPaused;
    }
}

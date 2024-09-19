using CronometerTask.Domain.Cronometers;
using CronometerTask.UI.Services;
using CronoTask.UI.Common;
using CronoTask.UI.ViewModel;
using System.Windows.Input;

namespace CronometerTask.UI.Cronometer
{
    public class CronometerVm : ViewModelBase
    {
        private const string InitialTimeValue = "00";

        private string? _seconds;
        private string? _minutes;
        private string? _hours;
        //private Domain.Cronometer.ICronometer _cronometer;
        private CronometerService _cronometerService;
        private readonly ICommand? _startCommand;
        private readonly ICommand? _pauseCommand;
        private readonly ICommand? _stopCommand;

        public CronometerVm()
        {
            SetInitialClockParameters();
            ICronometerTimeMeasure timer = new SecondsTimeMeasure();
            var cronometer = Domain.Cronometers.Cronometer.CreateCronometer(timer);
            _cronometerService = new CronometerService(cronometer);

            _startCommand = new RelayCommand((param) => {
                _cronometerService.Start();
                NotifyAvailabilityPropertiesChanged();
            }, param => CanStart);

            _pauseCommand = new RelayCommand((param) => 
            {
                _cronometerService.Pause();
                NotifyAvailabilityPropertiesChanged();
            }, param => CanStop);

            _stopCommand = new RelayCommand((param) =>
            {
                _cronometerService.Stop();
                NotifyAvailabilityPropertiesChanged();
            }, param => CanReset);

            cronometer.UnitOfTimeElapsed += (sender, args) => {
                if (args.CronometerTime == null) return;

                Seconds = args.CronometerTime.Time.Second < 10 ? $"0{args.CronometerTime.Time.Second}" : args.CronometerTime.Time.Second.ToString();
                Minutes = args.CronometerTime.Time.Minute < 10 ? $"0{args.CronometerTime.Time.Minute}" : args.CronometerTime.Time.Minute.ToString();
                Hours = args.CronometerTime.Time.Hour < 10 ? $"0{args.CronometerTime.Time.Hour}" : args.CronometerTime.Time.Hour.ToString();
            };
        }

        private void SetInitialClockParameters()
        {
            Seconds = InitialTimeValue;
            Minutes = InitialTimeValue;
            Hours = InitialTimeValue;
        }

        private void NotifyAvailabilityPropertiesChanged()
        {
            OnPropertyChanged(nameof(CanStart));
            OnPropertyChanged(nameof(CanStop));
            OnPropertyChanged(nameof(CanReset));
        }

        #region Commands

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

        #endregion

        #region Properties

        public string? Seconds
        {
            get => _seconds;
            private set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        public string? Minutes
        {
            get => _minutes;
            private set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        public string? Hours
        {
            get => _hours;
            private set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        public bool CanStart => !_cronometerService.IsRunning;

        public bool CanStop => _cronometerService.IsRunning;

        public bool CanReset => _cronometerService.IsPaused;

        #endregion

    }
}

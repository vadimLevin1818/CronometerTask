using CronometerTask.Domain.Common;
using CronometerTask.Domain.Cronometers;
using CronometerTask.UI.Services;
using CronoTask.UI.Common;
using CronoTask.UI.ViewModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace CronometerTask.UI.Cronometer
{
    public class CronometerVm : ViewModelBase
    {
        #region Private Members

        private const string InitialTimeValue = "00";
        private const string InitialStartButtonHeader = "Start";
        private const string PauseStartButtonHeader = "Restart";
        private const int NumericRepresentationThreashold = 10;

        private string? _seconds;
        private string? _minutes;
        private string? _hours;
        private readonly CronometerService _cronometerService;
        private readonly ICommand? _startCommand;
        private readonly ICommand? _pauseCommand;
        private readonly ICommand? _stopCommand;

        #endregion

        #region Ctor

        public CronometerVm()
        {
            SetInitialClockParameters();
            ICronometerTimeMeasure timer = new SecondsTimeMeasure();
            var cronometer = Domain.Cronometers.Cronometer.CreateCronometer(timer);
            _cronometerService = new CronometerService(cronometer);
            _cronometerService.SubscribeToUnitOfTimeElapsed(CronometerUnitOfTimeElapsed);

            _startCommand = new RelayCommand(Start, param => CanStart);
            _pauseCommand = new RelayCommand(Pause, param => CanStop);
            _stopCommand = new RelayCommand(Stop, param => CanReset);
        }

        #endregion

        #region Private Routines

        private void Start(object? param)
        {
            try
            {
                _cronometerService.Start();
                NotifyAvailabilityPropertiesChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show("There was an error starting the cronometer.");
            }
        }

        private void Pause(object? param)
        {
            try
            {
                _cronometerService.Pause();
                NotifyAvailabilityPropertiesChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show("There was an error pausing the cronometer.");
            }
        }

        private void Stop(object? param)
        {
            try
            {
                _cronometerService.Stop();
                NotifyAvailabilityPropertiesChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show("There was an error stopping the cronometer.");
            }
        }

        private void CronometerUnitOfTimeElapsed(object? sender, UnitOfTimeElapsedEventArgs args)
        {
            if (args?.CronometerTimeMeasure == null) return;

            Seconds = args.CronometerTimeMeasure.Time.Second < NumericRepresentationThreashold 
                ? $"0{args.CronometerTimeMeasure.Time.Second}" 
                : args.CronometerTimeMeasure.Time.Second.ToString();

            Minutes = args.CronometerTimeMeasure.Time.Minute < NumericRepresentationThreashold 
                ? $"0{args.CronometerTimeMeasure.Time.Minute}" 
                : args.CronometerTimeMeasure.Time.Minute.ToString();

            Hours = args.CronometerTimeMeasure.Time.Hour < NumericRepresentationThreashold 
                ? $"0{args.CronometerTimeMeasure.Time.Hour}" 
                : args.CronometerTimeMeasure.Time.Hour.ToString();
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
            OnPropertyChanged(nameof(StartButtonHeader));
        }

        #endregion

        #region Commands

        public ICommand? StartCommand => _startCommand;
        public ICommand? PauseCommand => _pauseCommand;
        public ICommand? StopCommand => _stopCommand;

        #endregion

        #region Properties

        public string StartButtonHeader => _cronometerService.IsPaused || _cronometerService.IsRunning
            ? PauseStartButtonHeader 
            : InitialStartButtonHeader;

        public string? Seconds
        {
            get => _seconds;
            private set => SetProperty(ref _seconds, value);
        }

        public string? Minutes
        {
            get => _minutes;
            private set => SetProperty(ref _minutes, value);
        }

        public string? Hours
        {
            get => _hours;
            private set => SetProperty(ref _hours, value);
        }

        public bool CanStart => !_cronometerService.IsRunning;

        public bool CanStop => _cronometerService.IsRunning;

        public bool CanReset => _cronometerService.IsPaused;

        #endregion

    }
}

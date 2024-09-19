﻿using CronometerTask.Domain.Common;
using CronometerTask.Domain.Cronometers;
using CronometerTask.UI.Services;
using CronoTask.UI.Common;
using CronoTask.UI.ViewModel;
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
        private CronometerService _cronometerService;
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

            cronometer.UnitOfTimeElapsed += CronometerUnitOfTimeElapsed;
        }

        #endregion

        #region Private Routines

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

        public string StartButtonHeader => _cronometerService.IsPaused || _cronometerService.IsRunning
            ? PauseStartButtonHeader 
            : InitialStartButtonHeader;

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

using System.Windows;
using System.Windows.Controls;

namespace CronometerTask.Resources.Controls
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class ClockControl : UserControl
    {
        public ClockControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register(
                "Seconds", typeof(string),
                typeof(ClockControl));

        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register(
                "Minutes", typeof(string),
                typeof(ClockControl));

        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register(
            "Hours", typeof(string),
            typeof(ClockControl), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        public string Seconds
        {
            get => (string)GetValue(SecondsProperty);
            set => SetValue(SecondsProperty, value);
        }

        public string Minutes
        {
            get => (string)GetValue(MinutesProperty);
            set => SetValue(MinutesProperty, value);
        }

        public string Hours
        {
            get => (string)GetValue(HoursProperty);
            set => SetValue(HoursProperty, value);
        }
    }
}

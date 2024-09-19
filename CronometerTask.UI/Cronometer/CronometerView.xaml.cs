using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CronometerTask.UI.Cronometer
{
    public partial class CronometerView : UserControl
    {
        public CronometerView()
        {
            InitializeComponent();
            DataContext = new CronometerVm();
        }
    }
}

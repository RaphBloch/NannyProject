using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;

namespace MAIN
{
    /// <summary>
    /// Interaction logic for DayControl.xaml
    /// </summary>
    public partial class DayControl : UserControl
    {

        //private DayPlanning dp;
        //public DayPlanning DayPlan
        //{
        //    get { return dp; }
        //    set
        //    {
        //        dp = value;
        //        DayCheck.IsChecked = dp.Selected;
        //        Start.Time = dp.Start;
        //        End.Time = dp.End;
        //    }
        //}




        public DayPlanning DayPlan
        {
            get { return (DayPlanning)GetValue(DayPlanProperty); }
            set { SetValue(DayPlanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DayPlan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayPlanProperty =
            DependencyProperty.Register("DayPlan",
                typeof(DayPlanning),
                typeof(DayControl),
                new PropertyMetadata(null/*new DayPlanning(false, "", DateTime.Parse("00:00"), DateTime.Parse("00:00"))*/, 
                    DayPlanPropertyChangedCallback));

        public static void DayPlanPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DayControl THIS = d as DayControl;
            THIS.DataContext = THIS.DayPlan;
        }

        public DayControl()
        {
            InitializeComponent();
            //DayPlan = new DayPlanning(false, "Monday", Start.tt, End.tt);

            //  organize events
            Start.TimeChanged += this.TimeChangeFunc;
            End.TimeChanged += this.AdaptDayPlanning;

            DataContext = DayPlan;
        }

        /// <summary>
        /// Function to select the current day from outside
        /// </summary>
        public void SelectDay()
        {
            DayPlan.Selected = true;
            DayCheck.IsChecked = true;
        }
        public void UnSelectDay()
        {
            DayPlan.Selected = false;
            DayCheck.IsChecked = false;
        }


        /// <summary>
        /// Event for checking the day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckDay(object sender, RoutedEventArgs e)
        {
            DayName.FontWeight = FontWeights.Bold;
            Start.Visibility = Visibility.Visible;
            End.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Event for uncheck the day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UncheckDay(object sender, RoutedEventArgs e)
        {
            DayName.FontWeight = FontWeights.ExtraLight;
            Start.Visibility = Visibility.Collapsed;
            End.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// To adapt the end time according to the start time
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void TimeChangeFunc(object o, EventArgs e)
        {
            if (!(e is TimeEventArgs))
                return;
            TimeEventArgs args = (TimeEventArgs)e;
            End.MinTime = Start.tt;
        }

        /// <summary>
        /// Adapt the day planning
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void AdaptDayPlanning(object o, EventArgs e)
        {
            if (!(e is TimeEventArgs))
                return;
            TimeEventArgs args = (TimeEventArgs)e;
        }
    }
}

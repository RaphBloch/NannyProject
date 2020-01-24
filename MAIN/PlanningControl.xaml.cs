
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
    /// Interaction logic for PlanningControl.xaml
    /// </summary>
    public partial class PlanningControl : UserControl
    {
        /// <summary>
        /// To check if at least one day is selected
        /// </summary>
        public bool IsOneSelected
        {
            get
            {
                for (int i = 0; i < 7; i++)
                {
                    if (Plan.Plan[i].Selected && Plan.Plan[i].Start < Plan.Plan[i].End)
                        return true;
                }

                return false;
            }
        }




        public DayPlanning Days0
        {
            get { return (DayPlanning)GetValue(Days0Property); }
            set { SetValue(Days0Property, value); }
        }

        // Using a DependencyProperty as the backing store for Days.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Days0Property =
            DependencyProperty.Register("Days0", 
                typeof(DayPlanning), 
                typeof(PlanningControl), 
                new PropertyMetadata(new DayPlanning(false, "Sunday", DateTime.Parse("00:00"), DateTime.Parse("00:00"))));



        public DayPlanning Days1
        {
            get { return (DayPlanning)GetValue(Days1Property); }
            set { SetValue(Days1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Days1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Days1Property =
            DependencyProperty.Register("Days1", 
                typeof(DayPlanning), 
                typeof(PlanningControl), 
                new PropertyMetadata(new DayPlanning(false, "Monday", DateTime.Parse("00:00"), DateTime.Parse("00:00"))));



        public DayPlanning Days2
        {
            get { return (DayPlanning)GetValue(Days2Property); }
            set { SetValue(Days2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Days2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Days2Property =
            DependencyProperty.Register("Days2", 
                typeof(DayPlanning), 
                typeof(PlanningControl), 
                new PropertyMetadata(new DayPlanning(false, "Tuesday", DateTime.Parse("00:00"), DateTime.Parse("00:00"))));




        public DayPlanning Days3
        {
            get { return (DayPlanning)GetValue(Days3Property); }
            set { SetValue(Days3Property, value); }
        }

        // Using a DependencyProperty as the backing store for Days3.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Days3Property =
            DependencyProperty.Register("Days3", 
                typeof(DayPlanning), 
                typeof(PlanningControl), 
                new PropertyMetadata(new DayPlanning(false, "Wednesday", DateTime.Parse("00:00"), DateTime.Parse("00:00"))));




        public DayPlanning Days4
        {
            get { return (DayPlanning)GetValue(Days4Property); }
            set { SetValue(Days4Property, value); }
        }

        // Using a DependencyProperty as the backing store for Days4.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Days4Property =
            DependencyProperty.Register("Days4", 
                typeof(DayPlanning), 
                typeof(PlanningControl), 
                new PropertyMetadata(new DayPlanning(false, "Thursday", DateTime.Parse("00:00"), DateTime.Parse("00:00"))));




        public DayPlanning Days5
        {
            get { return (DayPlanning)GetValue(Days5Property); }
            set { SetValue(Days5Property, value); }
        }

        // Using a DependencyProperty as the backing store for Days5.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Days5Property =
            DependencyProperty.Register("Days5", 
                typeof(DayPlanning), 
                typeof(PlanningControl), 
                new PropertyMetadata(new DayPlanning(false, "Friday", DateTime.Parse("00:00"), DateTime.Parse("00:00"))));




        public DayPlanning Days6
        {
            get { return (DayPlanning)GetValue(Days6Property); }
            set { SetValue(Days6Property, value); }
        }

        // Using a DependencyProperty as the backing store for Days6.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Days6Property =
            DependencyProperty.Register("Days6", 
                typeof(DayPlanning), 
                typeof(PlanningControl), 
                new PropertyMetadata(new DayPlanning(false, "Saturday", DateTime.Parse("00:00"), DateTime.Parse("00:00"))));










        public Planning Plan
        {
            get { return (Planning)GetValue(PlanProperty); }
            set { SetValue(PlanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Plan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlanProperty =
            DependencyProperty.Register("Plan", 
                typeof(Planning), typeof(PlanningControl), 
                new PropertyMetadata(null));


        public DayPlanning[] Days
        {
            get { return new DayPlanning[] { Days0, Days1, Days2, Days3, Days4, Days5, Days6 }; }
        }


        //public Planning Plan
        //{
        //    get
        //    {
        //        return new Planning(new DayPlanning[]
        //        {   _sunday.DayPlan,
        //            _monday.DayPlan,
        //            _tuesday.DayPlan,
        //            _wednesday.DayPlan,
        //            _thursday.DayPlan,
        //            _friday.DayPlan,
        //            _saturday.DayPlan,
        //        });
        //    }

        //    set
        //    {
        //        AdaptdDay(_sunday, value.Plan[0]);
        //        AdaptdDay(_monday, value.Plan[1]);
        //        AdaptdDay(_tuesday, value.Plan[2]);
        //        AdaptdDay(_wednesday, value.Plan[3]);
        //        AdaptdDay(_thursday, value.Plan[4]);
        //        AdaptdDay(_friday, value.Plan[5]);
        //        AdaptdDay(_saturday, value.Plan[6]);
        //    }
        //}

        private static void AdaptdDay(DayControl daycontrol, DayPlanning dayplanning)
        {
            daycontrol.DayPlan.Selected = dayplanning.Selected;
            daycontrol.DayPlan = dayplanning;
        }

        public PlanningControl()
        {
            InitializeComponent();

            _sunday.DataContext = Days0;
            _monday.DataContext = Days1;
            _tuesday.DataContext = Days2;
            _wednesday.DataContext = Days3;
            _thursday.DataContext = Days4;
            _friday.DataContext = Days5;
            _saturday.DataContext = Days6;

        }

        /// <summary>
        /// To check every days
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckAllDays(object sender, RoutedEventArgs e)
        {
            Days0.Selected = true;
            Days1.Selected = true;
            Days2.Selected = true;
            Days3.Selected = true;
            Days4.Selected = true;
            Days5.Selected = true;
            Days6.Selected = true;
        }

        /// <summary>
        /// To uncheck all days
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UncheckAllDays(object sender, RoutedEventArgs e)
        {

            Days0.Selected = false;
            Days1.Selected = false;
            Days2.Selected = false;
            Days3.Selected = false;
            Days4.Selected = false;
            Days5.Selected = false;
            Days6.Selected = false;
        }
    }
}

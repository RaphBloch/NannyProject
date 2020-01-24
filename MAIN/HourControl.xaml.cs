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
    /// Interaction logic for HourControl.xaml
    /// </summary>
    public partial class HourControl : UserControl
    {
        //  FIELDS
        private string h;
        public string Hour
        {
            get { return h; }
            set
            {
                int hr;
                try
                {
                    string str = value;
                    hr = int.Parse(str);
                    if (hr < 0 || hr > 23)
                        throw new Exception();
                    if (hr < 10)
                        value = "0" + hr.ToString();
                    h = value;
                }
                catch
                {
                    MessageBox.Show(
                        "Only numbers between 0 and 23.",
                        "WARNING",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    h = "00";
                    hour.Content = h;
                }
                CheckHour();
            }
        }

        private string m;
        public string Minute
        {
            get { return m; }
            set
            {
                int min;
                try
                {
                    string str = value;
                    min = int.Parse(str);
                    if (min < 0 || min > 60)
                        throw new Exception();
                    if (min < 10)
                        value = "0" + min.ToString();
                    m = value;
                }
                catch
                {
                    MessageBox.Show(
                        "Only numbers between 0 and 59.",
                        "WARNING",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    m = "00";
                    minute.Content = m;
                }
                CheckMinute();
            }
        }

        private DateTime t;
        public DateTime Time
        {
            get { return tt; }
            set { tt = value; }
        }

        private DateTime minT;
        public DateTime MinTime
        {
            get { return minT; }
            set
            {
                minT = value;
                if (Time < minT)
                    Time = minT;
                CheckMinute();
                CheckHour();
            }
        }




        public string hh
        {
            get { return (string)GetValue(hhProperty); }
            set { SetValue(hhProperty, value); }
        }

        // Using a DependencyProperty as the backing store for hh.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty hhProperty =
            DependencyProperty.Register("hh", 
                typeof(string), 
                typeof(HourControl), 
                new PropertyMetadata("00", HourChangedCallBack));

        public static void HourChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int hr;
            HourControl THIS = d as HourControl;

            string str = (string)e.NewValue;
            string final = "";
            hr = int.Parse(str);
            if (hr < 0 || hr > 23)
                throw new Exception();
            if (hr < 10)
                final = "0" + hr.ToString();
            else
                final = hr.ToString();
            THIS.hh = final;
            THIS.CheckHour();
        }


        public string mm
        {
            get { return (string)GetValue(mmProperty); }
            set { SetValue(mmProperty, value); }
        }

        // Using a DependencyProperty as the backing store for mm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mmProperty =
            DependencyProperty.Register("mm", 
                typeof(string), 
                typeof(HourControl), 
                new PropertyMetadata("00", MinuteChangedCallBack));

        public static void MinuteChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int min;
            HourControl THIS = d as HourControl;
            try
            {
                string str = (string)e.NewValue;
                string final = "";

                min = int.Parse(str);
                if (min < 0 || min > 60)
                    throw new Exception();
                if (min < 10)
                    final = "0" + min.ToString();
                else
                    final = min.ToString();
                THIS.mm = final;
            }
            catch
            {
                MessageBox.Show(
                    "Only numbers between 0 and 59.",
                    "WARNING",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            THIS.CheckMinute();
        }




        public DateTime tt
        {
            get { return (DateTime)GetValue(ttProperty); }
            set { SetValue(ttProperty, value); }
        }

        // Using a DependencyProperty as the backing store for tt.  *
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ttProperty =
            DependencyProperty.Register("tt", 
                typeof(DateTime), 
                typeof(HourControl), 
                new PropertyMetadata(DateTime.Parse("00:00"), TimeChangedCallBack));


        public static void TimeChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HourControl THIS = d as HourControl;

            THIS.tt = (DateTime)e.NewValue;

            THIS.hh = THIS.tt.Hour.ToString();
            THIS.hour.Content = THIS.hh;
            THIS.mm = THIS.tt.Minute.ToString();
            THIS.minute.Content = THIS.mm;

            TimeEventArgs args = new TimeEventArgs(THIS.tt);
            THIS.OnTimeChanged(args);
        }




        public event EventHandler TimeChanged;

        /// <summary>
        /// Event for time changed
        /// </summary>
        /// <param name="args"></param>
        public void OnTimeChanged(TimeEventArgs args)
        {
            TimeChanged?.Invoke(this, args);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public HourControl()
        {
            InitializeComponent();
            hour.DataContext = this;
            minute.DataContext = this;

            tt = DateTime.Parse("00:00");
            MinTime = tt;
        }

        /// <summary>
        /// Set hour + 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddHour(object sender, RoutedEventArgs e)
        {
            int hr = int.Parse(hh);

            //  if 0 <= hour <= 22
            if (hr >= 0 && hr <= 22)
            {
                hr++;
                Time = DateTime.Parse(hr.ToString() + ":" + mm);
            }
        }

        /// <summary>
        /// Set hour - 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveHour(object sender, RoutedEventArgs e)
        {
            int hr = int.Parse(hh);

            //  if 0 <= hour <= 22
            if ((hr > 0 && hr <= 23))
            {
                hr--;
                DateTime newDate = DateTime.Parse(hr.ToString() + ":" + mm);
                if (newDate >= MinTime)
                    Time = newDate;
            }
        }

        /// <summary>
        /// To display or not the buttons for hour
        /// </summary>
        void CheckHour()
        {
            int hour = int.Parse(hh);

            //  check hour
            add_hour.IsEnabled = true;
            remove_hour.IsEnabled = true;
            if (hour == 23)
                add_hour.IsEnabled = false;
            if (hour == MinTime.Hour)
                remove_hour.IsEnabled = false;
        }

        /// <summary>
        /// To display or not the buttons for minutes
        /// </summary>
        void CheckMinute()
        {
            int minutes = int.Parse(mm);

            add_minute.IsEnabled = true;
            remove_minute.IsEnabled = true;

            if (minutes == 50 && int.Parse(hh) == 23)
                add_minute.IsEnabled = false;

            int hr = int.Parse(hh);

            if (hr == MinTime.Hour && minutes == MinTime.Minute)
                remove_minute.IsEnabled = false;
        }

        /// <summary>
        /// To add 10 minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMinutes(object sender, RoutedEventArgs e)
        {
            int min = int.Parse(mm);

            if(min%10 != 0)
                min += 10 - min % 10;
            //  if 0 <= hour <= 22
            if (min >= 0 && min <= 50)
            {
                min += 10;
                if (min >= 60)
                {
                    min = 0;
                    AddHour(this, new RoutedEventArgs());
                }
                Time = DateTime.Parse(hh + ":" + min.ToString());
            }
            CheckMinute();
        }


        /// <summary>
        /// To remove 10 minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveMinutes(object sender, RoutedEventArgs e)
        {
            int min = int.Parse(mm);

            
            if (min % 10 != 0)
            {
                min -= min % 10;
                Time = DateTime.Parse(hh + ":" + min.ToString());
            }
                
            //  if 0 <= hour <= 22
            else if (min >= 0 && min <= 60)
            {
                min -= 10;
                if (min == -10)
                {
                    min = 50;
                    RemoveHour(this, new RoutedEventArgs());
                }
                Time = DateTime.Parse(hh + ":" + min.ToString());
            }
            CheckMinute();
        }
    }
}

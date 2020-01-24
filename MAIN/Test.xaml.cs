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
using System.Windows.Shapes;
using BE;

namespace MAIN
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public BE.DayPlanning P { get; set; }

        public Test()
        {
            InitializeComponent();
            P = new BE.DayPlanning(true, "Monday", DateTime.Parse("00:00"), DateTime.Parse("10:00"));
            //planning.DataContext = P;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((new Planning(planning.Days).ToString()));
        }
    }
}

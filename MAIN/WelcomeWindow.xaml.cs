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

namespace MAIN
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// To enter into the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WelcomeContinueButton_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WelcomeWindow_keyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}

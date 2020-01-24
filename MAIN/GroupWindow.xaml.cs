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
    /// Interaction logic for GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        public GroupWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Do grouping on nannies according to minimum children's age
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNannyMin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckExistingNannies())
                    throw new Exception("Please, add at least one nanny to consult data about them.\nYou can consult our database to check which data already exists.");
                GroupNannyMin gnm = new GroupNannyMin();
                gnm.Source = App.bl.GroupAges(true);
                gnm.Background = new RadialGradientBrush(Colors.White, Colors.LightGray);
                this.page.Content = gnm;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        /// <summary>
        /// Do grouping on nannies according to maximum children's age
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMax_click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                if (!CheckExistingNannies())
                    throw new Exception("Please, add at least one nanny to consult data about them.\nYou can consult our database to check which data already exists.");
                GroupNannyMax gnm = new GroupNannyMax();
                gnm.Source = App.bl.GroupAges(false);
                gnm.Background = new RadialGradientBrush(Colors.White, Colors.LightGray);
                this.page.Content = gnm;
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ButtonContract_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckExistingContracts())
                    throw new Exception("Please, add at least one contract to consult data about them.\nYou can consult our database to check which data already exists.");
                GroupContractDistance gcm = new GroupContractDistance();
                gcm.Source = App.bl.GroupDistance();
                gcm.Background = new RadialGradientBrush(Colors.White, Colors.LightGray);
                this.page.Content = gcm;
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


        }

        /// <summary>
        /// Check existence of nanny in database
        /// </summary>
        /// <returns></returns>
        bool CheckExistingNannies()
        {
            return App.bl.GetAllNanny().Count() != 0;
        }

        bool CheckExistingContracts()
        {
            return App.bl.GetAllContract().Count() != 0;
        }
    }
}

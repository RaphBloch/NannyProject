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
using BL;
using BE;

namespace MAIN
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
            WelcomeWindow w = new WelcomeWindow();
            w.ShowDialog();
        }

        #region MainMenuGrid

        /// <summary>
        /// On click see data button
        /// Open search menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeDataButton_click(object sender, RoutedEventArgs e)
        {
            (new ManageData()).ShowDialog();
        }

            #region Add Person

        /// <summary>
        /// On click add mother button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMotherButton_click(object sender, RoutedEventArgs e)
        {
            AddWindow add_w = new AddWindow();
            MotherControl mother_c = new MotherControl();
            mother_c.Margin = new Thickness(10);
            add_w.myGrid.Children.Add(mother_c);
            Grid.SetRow(mother_c, 0);
            add_w.ShowDialog();
        }

        /// <summary>
        /// On click add child button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddChildButton_click(object sender, RoutedEventArgs e)
        {

            AddWindow add_w = new AddWindow();
            ChildControl child_c = new ChildControl();

            child_c.Margin = new Thickness(10);
            

            add_w.myGrid.Children.Add(child_c);
            Grid.SetRow(child_c, 0);
           
            add_w.ShowDialog();
            //(new AddChildWindow()).ShowDialog();
        }



        /// <summary>
        /// On click add nanny button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNannyButton_click(object sender, RoutedEventArgs e)
        {
            AddWindow add_w = new AddWindow();
            NannyControl nanny_c = new NannyControl();

            nanny_c.Margin = new Thickness(10);


            add_w.myGrid.Children.Add(nanny_c);
            Grid.SetRow(nanny_c, 0);

            add_w.ShowDialog();
        }

        /// <summary>
        /// To add a contract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (App.bl.GetAllChild().Count() == 0
                    || App.bl.GetAllNanny().Count() == 0)
                    throw new Exception("To create a contract you need at least one child and one nanny.\nYou can consult our database to check which data already exists.");

                AddWindow add_w = new AddWindow();
                ContractControl contract_c = new ContractControl();
                contract_c.HelpDataContext('a');

                contract_c.Margin = new Thickness(10);
                add_w.myGrid.Children.Add(contract_c);
                Grid.SetRow(contract_c, 0);

                add_w.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        #endregion

        #endregion


        
            
         
    }


}

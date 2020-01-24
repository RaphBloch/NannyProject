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
using BL;

namespace MAIN
{
    /// <summary>
    /// Interaction logic for ManageData.xaml
    /// </summary>
    public partial class ManageData : Window
    {
        int SelectedComponent;
        bool flag = false;


        public ManageData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On selected an other mother
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedMotherChanged()
        {
            MotherControl mother_c = null;
            if (SelectedComponent != 0) return;

            mother_c = new MotherControl(); //create user control without binding

            ItemDetails.Content = mother_c;
            mother_c.Margin = new Thickness(20);
            mother_c.UserTitle.Content = "Update the mother";
            mother_c.ButtonContent.Content = "Update";
            mother_c.IdTextBox.IsEnabled = false;
            mother_c.FamilyNameTextBox.IsEnabled = false;
            mother_c.FirstNameTextBox.IsEnabled = false;
            mother_c.DeleteButton.Visibility = Visibility.Visible;

            mother_c.OnDeletingItem += this.RefreshDataGrid;
            mother_c.OnUpdatingItem += this.RefreshDataGrid;

            //  binding
            mother_c.mother = new Mother((Mother)PersonDetails.SelectedItem);
            mother_c.DoDataContext();
            
        }


        /// <summary>
        /// On selected an other nanny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedNannyChanged()
        {
            NannyControl nanny_c = null;
            if (SelectedComponent != 2) return;
            nanny_c = new NannyControl();
            nanny_c.OnDeletingItem += this.RefreshDataGrid;

            ItemDetails.Content = nanny_c;
            nanny_c.Margin = new Thickness(20);
            nanny_c.UserTitle.Content = "Update the Nanny";
            nanny_c.ButtonContent.Content = "Update";
            nanny_c.IdTextBox.IsEnabled = false;
            nanny_c.DeleteButton.Visibility = Visibility.Visible;

            nanny_c.OnUpdatingItem += this.RefreshDataGrid;
            nanny_c.OnDeletingItem += this.RefreshDataGrid;

            //  binding
            nanny_c.nanny = new Nanny((Nanny)PersonDetails.SelectedItem);
            nanny_c.DoDataContext();
        }

        /// <summary>
        /// On selected an other child
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedChildChanged()
        {
            ChildControl child_c = null;
            if (SelectedComponent != 1) return;
            child_c = new ChildControl();
            child_c.OnDeletingItem += this.RefreshDataGrid;
            child_c.OnUpdatingItem += this.RefreshDataGrid;
            ItemDetails.Content = child_c;
            child_c.child = new Child((Child)PersonDetails.SelectedItem);
            child_c.AdaptToUpdateMode();

        }

        /// <summary>
        /// On select other contract
        /// </summary>
        private void OnSelectedContractChanged()
        {
            ContractControl contract_c = null;
            if (SelectedComponent != 3) return;
            contract_c = new ContractControl();
            contract_c.OnDeletingItem += this.RefreshDataGrid;
            contract_c.OnUpdatingItem += this.RefreshDataGrid;

            ItemDetails.Content = contract_c;
            contract_c.Margin = new Thickness(20);
            
            contract_c.UserTitle.Content = "Update the contract";
            contract_c.ButtonContent.Content = "Update";
            contract_c.DeleteButton.Visibility = Visibility.Visible;

            contract_c.contract = (Contract)ContractDetails.SelectedItem;
            if (contract_c.contract.Signed == false)
                contract_c.Background = new RadialGradientBrush(Colors.White, Colors.Red);
            contract_c.DataContext = new Contract(contract_c.contract);
            contract_c.DoDataContext();
            contract_c.ChildComboBox.IsEnabled = false;
            contract_c.NannyComboBox.IsEnabled = false;
            contract_c.ContractStart.IsEnabled = false;

            contract_c.Signed.Visibility = Visibility.Visible;
            contract_c.SignedCheckBox.Visibility = Visibility.Visible;
            contract_c.Distance.Visibility = Visibility.Visible;
            contract_c.ContractDistance.Visibility = Visibility.Visible;
            contract_c.Cost.Visibility = Visibility.Visible;
            contract_c.SalaryText.Visibility = Visibility.Visible;

            
        }

         
        /// <summary>
        /// For refreshing datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshDataGrid(object sender, EventArgs e)
        {
            flag = false;
            if (PersonDetails.SelectedItem != null || ContractDetails.SelectedItem != null)
            {
                ItemDetails.Content = null;
                PersonDetails.SelectedItem = null;
                ContractDetails.SelectedItem = null;
            }
                
            switch (SelectedComponent)
            {
                case 0: //  mother
                    PersonDetails.ItemsSource = App.bl.GetAllMother().Where(x => x != null);
                    PersonDetails.Visibility = Visibility.Visible;
                    ContractDetails.Visibility = Visibility.Hidden;
                    ContractDetails.SelectedItem = null;
                    break;

                case 1: //  child
                    PersonDetails.ItemsSource = App.bl.GetAllChild().Where(x => x != null);
                    PersonDetails.Visibility = Visibility.Visible;
                    ContractDetails.Visibility = Visibility.Hidden;
                    ContractDetails.SelectedItem = null;
                    break;

                case 2: //  nanny
                    PersonDetails.ItemsSource = App.bl.GetAllNanny().Where(x => x != null);
                    PersonDetails.Visibility = Visibility.Visible;
                    ContractDetails.Visibility = Visibility.Hidden;
                    ContractDetails.SelectedItem = null;
                    break;

                case 3: //  contract
                    ContractDetails.ItemsSource = App.bl.GetAllContract().Where(x => x != null);
                    PersonDetails.SelectedItem = null;
                    ContractDetails.Visibility = Visibility.Visible;
                    PersonDetails.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }

        private void AdaptButton(int i)
        {
            MotherButton.IsEnabled = i != 0;
            ChildButton.IsEnabled = i != 1;
            NannyButton.IsEnabled = i != 2;
            ContractButton.IsEnabled = i != 3;
        }


        /// <summary>
        /// On clicking see mother button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeMothers_click(object sender, RoutedEventArgs e)
        {
            SelectedComponent = 0;
            RefreshDataGrid(this, new EventArgs());
            AdaptButton(SelectedComponent);
        }


        /// <summary>
        /// On clicking see children button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeChildren_click(object sender, RoutedEventArgs e)
        {
            SelectedComponent = 1;
            RefreshDataGrid(this, new EventArgs());
            AdaptButton(SelectedComponent);
           
        }

        /// <summary>
        /// On clicking see nannies button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeNannies_click(object sender, RoutedEventArgs e)
        {
            SelectedComponent = 2;
            RefreshDataGrid(this, new EventArgs());
            AdaptButton(SelectedComponent);

        }

        /// <summary>
        /// On clicking see contract button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeContracts_click(object sender, RoutedEventArgs e)
        {
            SelectedComponent = 3;
            RefreshDataGrid(this, new EventArgs());
            AdaptButton(SelectedComponent);
        }

        

        /// <summary>
        /// On selectedItem changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {

            if (PersonDetails.SelectedItem == null && ContractDetails.SelectedItem == null) return;
            if (flag)
                ItemDetails.Content = null;
            flag = true;
            switch (SelectedComponent)
            {
                case 0: //  mother
                    OnSelectedMotherChanged();
                    break;

                case 1: //  child
                    OnSelectedChildChanged();
                    break;

                case 2: //  nanny
                    OnSelectedNannyChanged();
                    break;

                case 3: //  contract
                    OnSelectedContractChanged();
                    break;
                default:
                    break;
            }
        }


        private void OpenGroupWindow_click(object sender, RoutedEventArgs e)
        {
            (new GroupWindow()).ShowDialog();
        }

        private void AvoidDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

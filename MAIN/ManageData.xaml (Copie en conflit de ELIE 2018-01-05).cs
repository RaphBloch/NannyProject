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
        Mother m;
        Nanny n;
        Child child;
        Contract contract;
        int SelectedComponent;


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
                mother_c = new MotherControl();
                ItemDetails.Children.Add(mother_c);
                mother_c.Margin = new Thickness(20);
                mother_c.UserTitle.Content = "Update the mother";
                mother_c.ButtonContent.Content = "Update";
                mother_c.IdTextBox.IsEnabled = false;
                mother_c.DeleteButton.Visibility = Visibility.Visible;

            mother_c.OnDeletingItem += this.RefreshDataGrid;

            mother_c.mother = (Mother)PersonDetails.SelectedItem;
            mother_c.DataContext = mother_c.mother; //new Child(child_c.child);
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
            ItemDetails.Children.Add(child_c);
            child_c.Margin = new Thickness(20);
            child_c.UserTitle.Content = "Update the child";
            child_c.ButtonContent.Content = "Update";
            child_c.IdTextBox.IsEnabled = false;
            child_c.DeleteButton.Visibility = Visibility.Visible;

            child_c.child = (Child)PersonDetails.SelectedItem;
            child_c.DataContext = child_c.child; //new Child(child_c.child);
            if (child_c.MotherComboBox.SelectedItem != null)
                child_c.MotherComboBox.SelectedItem = App.bl.GetAllMother().Where(x => x.ID == child_c.child.MotherID).First();
            child_c.MotherComboBox.IsEnabled = false;

        }

        /// <summary>
        /// For refreshing datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshDataGrid(object sender, EventArgs e)
        {
            flag = false;
            if (PersonDetails.SelectedItem != null)
                ItemDetails.Children.RemoveAt(0);
            switch (SelectedComponent)
            {
                case 0: //  mother
                    PersonDetails.ItemsSource = App.bl.GetAllMother().Where(x => x != null);
                    PersonDetails.Visibility = Visibility.Visible;
                    break;

                case 1: //  child
                    PersonDetails.ItemsSource = App.bl.GetAllChild().Where(x => x != null);
                    PersonDetails.Visibility = Visibility.Visible;
                    break;

                case 2: //  nanny
                    PersonDetails.ItemsSource = App.bl.GetAllNanny().Where(x => x != null);
                    PersonDetails.Visibility = Visibility.Visible;
                    break;

                case 3: //  contract
                    //PersonDetails.Visibility = Visibility.Visible;
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
            AdaptButton(SelectedComponent);
            flag = false;
            if (PersonDetails.SelectedItem != null)
                ItemDetails.Children.RemoveAt(0);

            PersonDetails.ItemsSource = App.bl.GetAllChild().Where(x => x != null);
            PersonDetails.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// On clicking see nannies button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeNannies_click(object sender, RoutedEventArgs e)
        {
            SelectedComponent = 2;
            AdaptButton(SelectedComponent);
            flag = false;
            PersonDetails.Visibility = Visibility.Visible;
            PersonDetails.ItemsSource = App.bl.GetAllNanny().Where(x => x != null);
            
        }

        /// <summary>
        /// On clicking see contract button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeContracts_click(object sender, RoutedEventArgs e)
        {
            SelectedComponent = 3;
            AdaptButton(SelectedComponent);
            flag = false;
            InitializeComponent();
        }

        bool flag = false;
        /// <summary>
        /// On selectedItem changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {

            if (PersonDetails.SelectedItem == null) return;
            if(flag)
                ItemDetails.Children.RemoveAt(0);
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
                    break;

                case 3: //  contract
                    break;
                default:
                    break;
            }
        }
    }
}

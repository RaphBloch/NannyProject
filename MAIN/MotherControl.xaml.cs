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
    /// Interaction logic for MotherControl.xaml
    /// </summary>
    public partial class MotherControl : UserControl
    {
        public Mother mother { get; set; }

        public EventHandler OnDeletingItem = null;
        public EventHandler OnUpdatingItem = null;


        /// <summary>
        /// Constructor
        /// </summary>
        public MotherControl()
        {
            InitializeComponent();

            if((string)ButtonContent.Content == "Add")
                DoDataContext();
        }

        /// <summary>
        /// Do data context
        /// </summary>
        public void DoDataContext()
        {
            if (mother == null)
                mother = new Mother();

            DataContext = mother;
            AddressTextBox.DataContext = mother.Request;
            ResearchAddressTextBox.DataContext = mother.Request;
            DistanceWantedTextBox.DataContext = mother.Request;
            DistanceAcceptedTextBox.DataContext = mother.Request;

            MotherPlanning._sunday.DataContext = mother.Request.P.Plan[0];
            MotherPlanning._monday.DataContext = mother.Request.P.Plan[1];
            MotherPlanning._tuesday.DataContext = mother.Request.P.Plan[2];
            MotherPlanning._wednesday.DataContext = mother.Request.P.Plan[3];
            MotherPlanning._thursday.DataContext = mother.Request.P.Plan[4];
            MotherPlanning._friday.DataContext = mother.Request.P.Plan[5];
            MotherPlanning._saturday.DataContext = mother.Request.P.Plan[6];
        }

        /// <summary>
        /// On add/update mother
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MotherButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckInput();
                if ((string)ButtonContent.Content == "Add")
                {
                    App.bl.AddMother(mother);
                    mother = new BE.Mother();
                    DoDataContext();
                    throw new Exception("Added");
                }
                else
                {
                    App.bl.UpdateMother(mother);
                    UpdateItem(new EventArgs());
                    throw new Exception("Updated");
                }

            }
            catch (Exception exception)
            {
                string m = (exception.Message == "Added") ? "This mother was added successfully." : "This mother was updated successfully.";

                if (exception.Message == "Added" || exception.Message == "Updated")
                    MessageBox.Show(m, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// On value ID TEXTBOX changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IdTextBox.Text == "") return;
            try
            {
                int id = int.Parse(IdTextBox.Text);
            }
            catch
            {
                IdTextBox.Text = "";
            }
        }

        private void CheckInput()
        {
            CheckFields.IsValidID(int.Parse(IdTextBox.Text));
            CheckFields.IsValidAddress(AddressTextBox.Text);
            CheckFields.IsValidAcceptedAndWantedDistance(DistanceWantedTextBox.Text, DistanceAcceptedTextBox.Text);
        }

        /// <summary>
        /// Delete the current mother
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.bl.DeleteMother(int.Parse(IdTextBox.Text));
                DeleteItem(new EventArgs());
                mother = new Mother();
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        /// <summary>
        /// Invoke event
        /// </summary>
        /// <param name="e"></param>
        private void DeleteItem(EventArgs e)
        {
            OnDeletingItem?.Invoke(this, e);
        }


        private void UpdateItem(EventArgs e)
        {
            OnUpdatingItem?.Invoke(this, e);
        }

        /// <summary>
        /// Check if enter only digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWDistanceChanged(object sender, TextChangedEventArgs e)
        {
            if (DistanceWantedTextBox.Text == "") return;
            try
            {
                int id = int.Parse(DistanceWantedTextBox.Text);
            }
            catch
            {
                DistanceWantedTextBox.Text = "";
            }
        }


        /// <summary>
        /// Check if enter only digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnADistanceChanged(object sender, TextChangedEventArgs e)
        {
            if (DistanceAcceptedTextBox.Text == "") return;
            try
            {
                int id = int.Parse(DistanceAcceptedTextBox.Text);
            }
            catch
            {
                DistanceAcceptedTextBox.Text = "";
            }
        }
    }
}

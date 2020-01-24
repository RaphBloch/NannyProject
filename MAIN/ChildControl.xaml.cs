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
    /// Interaction logic for ChildControl.xaml
    /// </summary>
    public partial class ChildControl : UserControl
    {
        public Child child { get; set; }

        public EventHandler OnDeletingItem = null;
        public EventHandler OnUpdatingItem = null;

        /// <summary>
        /// Invoke event
        /// </summary>
        /// <param name="e"></param>
        private void DeleteItem(EventArgs e)
        {
            OnDeletingItem?.Invoke(this, e);
        }

        /// <summary>
        /// Invoke event
        /// </summary>
        /// <param name="e"></param>
        private void UpdateItem(EventArgs e)
        {
            OnUpdatingItem?.Invoke(this, e);
        }



        /// <summary>
        /// Constructor
        /// </summary>
        public ChildControl()
        {
            if(child == null)
                child = new Child();
            DataContext = child;
            InitializeComponent();
            //  about date picker
            ChildBirthDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            ChildBirthDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
            ChildBirthDatePicker.SelectedDate = DateTime.Parse("01/01/2012");
            MotherComboBox.ItemsSource = App.bl.GetAllMother();
            MotherComboBox.DisplayMemberPath = "SimplePresentation";
            MotherComboBox.SelectedValuePath = "ID";
            
        }

        public void AdaptToAddMode()
        {
            ChildBirthDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Now, DateTime.Parse("01/01/3000")));
            ChildBirthDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/1111"), DateTime.Parse("01/01/2000")));
            ChildBirthDatePicker.SelectedDate = DateTime.Parse("01/01/2012");
            MotherComboBox.ItemsSource = App.bl.GetAllMother();
            MotherComboBox.DisplayMemberPath = "SimplePresentation";
            MotherComboBox.SelectedValuePath = "ID";
        }

        /// <summary>
        /// Easy adapt user control to the update mode
        /// </summary>
        public void AdaptToUpdateMode()
        {
            Margin = new Thickness(20);
            UserTitle.Content = "Update the child";
            ButtonContent.Content = "Update";
            IdTextBox.IsEnabled = false;
            FirstNameTextBox.IsEnabled = false;
            DeleteButton.Visibility = Visibility.Visible;

            DataContext = child;
            if (MotherComboBox.SelectedItem != null)
                MotherComboBox.SelectedItem = App.bl.GetAllMother(x => x.ID == child.MotherID).FirstOrDefault();
            MotherComboBox.IsEnabled = false;
        }

        /// <summary>
        /// To add the child
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildButton_click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if ((string)ButtonContent.Content == "Add")
                {
                    if (IdTextBox.Text.Count() != 5)
                        throw new Exception("The ID must be 5 numbers.");
                    App.bl.AddChild(child);
                    child = new Child();
                    DataContext = child;
                    throw new Exception("Added");

                }
                else
                {
                    App.bl.UpdateChild(child);
                    UpdateItem(new EventArgs());
                    throw new Exception("Updated");
                }
                    
            }
            catch (Exception exception)
            {
                string m = (exception.Message == "Added") ? "This child was added successfully." : "This child was updated successfully.";

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

                if (id < 10000 || id > 99999) ;

                if (MotherComboBox != null)
                    MotherComboBox.IsEnabled = true;
            }
            catch
            {
                if (MotherComboBox != null)
                    MotherComboBox.IsEnabled = false;
                IdTextBox.Text = "";
            }
        }

        /// <summary>
        /// Delete the current child
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.bl.DeleteChild(int.Parse(IdTextBox.Text));
                child = new Child();
                DeleteItem(new EventArgs());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        
    }
}

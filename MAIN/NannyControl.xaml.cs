using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Our value converter from boolean to int
    /// </summary>
    public class BooleanToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (boolValue)
                return 0;
            else
                return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if (intValue == 0)
                return true;
            else
                return false;
        }
    }


    /// <summary>
    /// Interaction logic for NannyControl.xaml
    /// </summary>
    public partial class NannyControl : UserControl
    {
        public Nanny nanny { get; set; }

         public EventHandler OnDeletingItem = null;
        public EventHandler OnUpdatingItem = null;

        private void DeleteItem(EventArgs e)
        {
            OnDeletingItem?.Invoke(this, e);
        }

        private void UpdateItem(EventArgs e)
        {
            OnUpdatingItem?.Invoke(this, e);
        }

        /// <summary>
        /// Doing data context
        /// </summary>
        public void DoDataContext()
        {
            if (nanny == null) return;

            DataContext = nanny;
            NannyPlanning.Plan = nanny.P;
            VacationTypeComboBox.ItemsSource = Enum.GetValues(typeof(BE.VacationType));

            SalaryType.ItemsSource = Enum.GetValues(typeof(BE.PaymentType));
            AddressTextBox.DataContext = nanny;


            NannyPlanning._sunday.DataContext = nanny.P.Plan[0];
            NannyPlanning._monday.DataContext = nanny.P.Plan[1];
            NannyPlanning._tuesday.DataContext = nanny.P.Plan[2];
            NannyPlanning._wednesday.DataContext = nanny.P.Plan[3];
            NannyPlanning._thursday.DataContext = nanny.P.Plan[4];
            NannyPlanning._friday.DataContext = nanny.P.Plan[5];
            NannyPlanning._saturday.DataContext = nanny.P.Plan[6];

        }

        public NannyControl()
        {
            InitializeComponent();
            if (nanny == null)
                nanny = new Nanny();
            
            if((string)ButtonContent.Content == "Add")
                DoDataContext();
        }

        /// <summary>
        /// On deleting nanny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.bl.DeleteNanny(int.Parse(IdTextBox.Text));
                DeleteItem(new EventArgs());
                nanny = new Nanny();
                throw new Exception("The nanny was deleted succesfully.");
            }
            catch (Exception exception)
            {
                if (exception.Message == "The nanny was deleted succesfully.")
                    MessageBox.Show(exception.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        /// <summary>
        /// On add/update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NannyButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckInput();
                if ((string)ButtonContent.Content == "Add")
                {
                    App.bl.AddNanny(nanny);
                    nanny = new Nanny();
                    DoDataContext();
                    throw new Exception("Added");
                }
                else
                {
                    App.bl.UpdateNanny(nanny);
                    UpdateItem(new EventArgs());
                    throw new Exception("Updated");
                }

            }
            catch (Exception exception)
            {
                string m = (exception.Message == "Added") ? "This nanny was added successfully." : "This nanny was updated successfully.";

                if(exception.Message == "Added" || exception.Message == "Updated")
                    MessageBox.Show(m, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// To check the input
        /// use our class check fields
        /// </summary>
        private void CheckInput()
        {
            CheckFields.IsValidID(int.Parse(IdTextBox.Text));
            CheckFields.IsValidAddress(AddressTextBox.Text);
            CheckFields.IsValidPositiveNumber(MaxAgeTextBox.Text);
            CheckFields.IsValidPositiveNumber(MinAgeTextBox.Text);
            CheckFields.IsValidPositiveNumber(FloorTextBox.Text);
            CheckFields.IsValidPositiveNumber(SeniorityTextBox.Text);
            CheckFields.IsValidPositiveNumber(MaxChildsTextBox.Text);
            CheckFields.IsValidPositiveNumber(SalaryText.Text);
        }

        /// <summary>
        /// On change salary text box type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalaryTextBox_textChanged(object sender, TextChangedEventArgs e)
        {
            if (SalaryText.Text == "") { SalaryType.IsEnabled = false; return; }
            try
            {
                if (SalaryText.Text.Last() == '.' || SalaryText.Text.Last() == ',') return;

                double salary = double.Parse(SalaryText.Text.Replace('.', ','));
                SalaryType.IsEnabled = true;
                //SalaryType.SelectedIndex = 0;

            }
            catch
            {
                SalaryType.IsEnabled = false;
                SalaryText.Text = "";
            }
        }

       
    }
}

  
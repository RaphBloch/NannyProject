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
using System.Threading;

namespace MAIN
{

   

    /// <summary>
    /// Interaction logic for ContractControl.xaml
    /// </summary>
    public partial class ContractControl : UserControl
    {
        public Contract contract { get; set; }

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

        public ContractControl()
        {
            InitializeComponent();
            contract = new Contract();
            
        }

        public void HelpDataContext(char a)
        {
            if (a == 'a') ButtonContent.Content = "Add";
            else ButtonContent.Content = "Update";

            DoDataContext();
        }

        /// <summary>
        /// To append data context
        /// </summary>
        public void DoDataContext()
        {
            if (contract == null) return;

            DataContext = contract;
            //  initialize dates just if in "add mode"

            
            if ((string)ButtonContent.Content == "Add")
            {
                    contract.Begin = (DateTime.Now).AddDays(1);
                    ContractStart.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/0"), DateTime.Now));
                    contract.End = (DateTime.Now).AddDays(2);
            }
            ContractEnd.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/0"), contract.Begin));

            //  child combo box
            ChildComboBox.ItemsSource = App.bl.GetAllChild();
            ChildComboBox.DisplayMemberPath = "SimplePresentation";
            ChildComboBox.SelectedValuePath = "ID";
            NannyComboBox.IsEnabled = false;

            SalaryType.ItemsSource = Enum.GetValues(typeof(BE.PaymentType));
            SalaryText.Content = String.Format("{0:0} ₪/month", contract.Salary);
            ContractDistance.Content = String.Format("{0} meters", contract.Distance);
        }

        /// <summary>
        /// On clicking event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ContractButton_click(object sender, RoutedEventArgs e)
        {
            if ((string)ButtonContent.Content == "Add")
                AddContractThreaded();
            else
                UpdateContractThreaded();

        }

        /// <summary>
        /// To update contract
        /// </summary>
        private void UpdateContractThreaded()
        {
            try
            {
                Contract c = new Contract(contract);
                if (((Nanny)(NannyComboBox.SelectedItem)).SalaryType != c.Payment)
                    throw new Exception("Please select an other payment type");
                App.bl.UpdateContract(c);
                UpdateItem(new EventArgs());
                throw new Exception("The contract was updated successfully.");
            }
            catch (Exception exception)
            {
                if (exception.Message == "The contract was updated successfully.")
                    MessageBox.Show(exception.Message, "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// To manage threads for google maps
        /// </summary>
        private void AddContractThreaded()
        {
            try
            {

                Contract c = new Contract(contract);
                if (((Nanny)(NannyComboBox.SelectedItem)).SalaryType != c.Payment)
                    throw new Exception("Please select an other payment type");
                App.bl.AddContract(c);
                contract = new Contract();
                DoDataContext();
                throw new Exception("The contract was added successfully.");
            }
            catch (Exception exception)
            {
                if (exception.Message == "The contract was added successfully.")
                    MessageBox.Show(exception.Message, "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        /// <summary>
        /// When the selection of the child in the combo box has changed,if there is no selected item 
        /// so it is not possible to select a nanny and change the items in the nanny combo box with 
        /// the nanny that correspond to the mother 's request of the child selected 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildComboBox_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Child selectedChild = ChildComboBox.SelectedItem as Child;

            //  if no selected child
            if (selectedChild == null)
            {
                NannyComboBox.IsEnabled = false;
                return;
            }


            //  get the current child and its mother
            //Child c = App.bl.GetAllChild(x=>x.ID == selectedChild.ID).FirstOrDefault();
            Mother m = App.bl.GetAllMother(x=> x.ID == selectedChild.MotherID).FirstOrDefault();

            NannyComboBox.ItemsSource = App.bl.PlanningAccordance(m.Request);
            NannyComboBox.DisplayMemberPath = "SimplePresentation";
            NannyComboBox.SelectedValuePath = "ID";

            if ((string)ButtonContent.Content == "Update")
                NannyComboBox.IsEnabled = false;
            else
                NannyComboBox.IsEnabled = true;
        }

        /// <summary>
        /// On changing the date of start, adapt the end to be one day more
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractStart_dateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime time = (DateTime)ContractStart.SelectedDate;
            ContractEnd.SelectedDate = time.AddDays(1);
            ContractEnd.BlackoutDates.Add(new CalendarDateRange(DateTime.Parse("01/01/0"), time));
        }


        /// <summary>
        /// On deleting contract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_click(object sender, RoutedEventArgs e)
        {

            try
            {
                App.bl.DeleteContract(contract.ID);
                DeleteItem(new EventArgs());
                contract = new Contract();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }


        
    }
}

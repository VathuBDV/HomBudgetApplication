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
using Budget;

namespace HomeBudgetGUI
{
    /// <summary>
    /// Interaction logic for ModifyDetails.xaml
    /// </summary>
    public partial class ModifyDetails : Window
    {
        List<Category> categories;
        HomeBudget budget;
        int expenseID;
        public ModifyDetails(HomeBudget budget, int expenseID)
        {
            this.budget = budget;
            this.expenseID = expenseID;
            InitializeComponent();
            
            //Code below for the Combobox
            categories = this.budget.categories.List();
            Categories.DisplayMemberPath = "Description";
            Categories.ItemsSource = categories;
        }

        private void ModifyExpbtn_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            string message;
            if (DatePick.SelectedDate == null)
            {
                MessageBox.Show("Please select a date in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (!double.TryParse(Amount.Text, out amount))
            {
                message = "Provided Amount Is Not In correct format: 0.00";
                MessageBox.Show(message, "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Convert.ToString(Desc.Text) == "")
            {
                MessageBox.Show("Please enter a description in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (Categories.SelectedItem == null)
            {
                MessageBox.Show("Please select a category in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DateTime date = (DateTime)DatePick.SelectedDate;

                Category cat = this.budget.categories.GetCategoryFromId(Categories.SelectedIndex + 1);
                //Depending on the type it is added as a negative or added as a positive
                if ((int)cat.Type == 2 || (int)cat.Type == 4)
                {
                    this.budget.expenses.UpdateProperties(expenseID, date, Categories.SelectedIndex + 1, -Convert.ToDouble(Amount.Text), Convert.ToString(Desc.Text));

                }
                if ((int)cat.Type == 1 || (int)cat.Type == 3)
                {
                    this.budget.expenses.UpdateProperties(expenseID, date, Categories.SelectedIndex + 1, Convert.ToDouble(Amount.Text), Convert.ToString(Desc.Text));

                }

                MessageBox.Show("Expense Modified Sucessfully");
                Close();
            }
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

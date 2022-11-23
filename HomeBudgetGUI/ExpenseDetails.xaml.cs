using Budget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HomeBudgetGUI
{
    /// <summary>
    /// Interaction logic for ExpenseDetails.xaml
    /// </summary>
    public partial class ExpenseDetails : Window
    {
        List<Category> categories;
        HomeBudget budget;
        //Only displays the descriptions of the categories in the Combobox
        public ExpenseDetails(HomeBudget budget)
        {
            this.budget = budget;
            
            InitializeComponent();
            categories = this.budget.categories.List();
            
            //Categories.DisplayMemberPath = "Id";
            Categories.DisplayMemberPath = "Description";
            //Categories.DisplayMemberPath = "Type";
            Categories.ItemsSource = categories;
            
        }

        //Verifies the inputted data then adds it if it is valid and clears only amount and description after entry is successful.
        private void AddExpbtn_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            string message;
            if(DatePick.SelectedDate == null)
            {
                MessageBox.Show("Please select a date in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            else if (!double.TryParse(Amount.Text, out amount))
            {
                message = "Provided Amount Is Not In correct format: 0.00";
                MessageBox.Show(message, "Invalid", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(Convert.ToString(Desc.Text) == "")
            {
                MessageBox.Show("Please enter a description in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(Categories.SelectedItem == null)
            {
                MessageBox.Show("Please select a category in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DateTime date = (DateTime)DatePick.SelectedDate;
                
                Category cat = this.budget.categories.GetCategoryFromId(Categories.SelectedIndex + 1);
                //Depending on the type it is added as a negative or added as a positive
                if ((int)cat.Type == 2 || (int)cat.Type == 4){
                    this.budget.expenses.Add(date, Categories.SelectedIndex + 1, -Convert.ToDouble(Amount.Text), Convert.ToString(Desc.Text));
                        
                }
                if ((int)cat.Type == 1 || (int)cat.Type == 3)
                {
                    this.budget.expenses.Add(date, Categories.SelectedIndex + 1, Convert.ToDouble(Amount.Text), Convert.ToString(Desc.Text));
                        
                }
                
                //If its paid by a credit card then it added a second expense at the same time
                if(CreditCheck.IsChecked == true)
                {
                    for(int i = 0; i < categories.Count; i++)
                    {
                        if(categories[i].Description == "Credit Card")
                        {
                            Categories.SelectedIndex = i;
                            break;
                        }
                    }
                    this.budget.expenses.Add(date, Categories.SelectedIndex + 1, Convert.ToDouble(Amount.Text), Convert.ToString(Desc.Text));
                }
                MessageBox.Show("Expense Added Sucessfully");
                Amount.Clear();
                Desc.Clear();
                CreditCheck.IsChecked = false;
            }
        }

        //Closes this specific window
        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
             Close();
        }

        //Confirms on the X button helped with: https://stackoverflow.com/questions/19589462/confirmation-when-closing-wpf-window-with-x-button-or-esc-key
        protected override void OnClosing(CancelEventArgs e)
        {
            double amount;
            if (DatePick.SelectedDate != null || double.TryParse(Amount.Text, out amount) || Convert.ToString(Desc.Text) != "" || Categories.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure you do not want to continue?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    base.OnClosing(e);
                }
                else
                {
                    e.Cancel = true;
                    //return;
                }
            }

        }
    }
}

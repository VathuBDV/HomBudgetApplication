using Budget;
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

namespace HomeBudgetGUI
{
    /// <summary>
    /// Interaction logic for AddCatORExp.xaml
    /// </summary>
    public partial class AddCatORExp : Window
    {
        private HomeBudget budget;
        public AddCatORExp(HomeBudget budget)
        {
            this.budget = budget;
            InitializeComponent();
        }

        //Sends the User to different windows depending if they want to add a category or expense or simply view them in the list.
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryDetails newCategoryDetails = new CategoryDetails(budget);
            newCategoryDetails.ShowDialog();
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            ExpenseDetails newExpenseDetails = new ExpenseDetails(budget);
            newExpenseDetails.ShowDialog();
        }

        private void ViewCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoriesList newCategoryList = new CategoriesList(budget);
            newCategoryList.ShowDialog();
        }

        private void ViewExpense_Click(object sender, RoutedEventArgs e)
        {
            ExpensesList newExpenseList = new ExpensesList(budget);
            newExpenseList.ShowDialog();
        }
    }
}

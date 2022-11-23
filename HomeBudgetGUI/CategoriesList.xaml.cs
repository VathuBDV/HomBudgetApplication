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
    /// Interaction logic for CategoriesList.xaml
    /// </summary>
    public partial class CategoriesList : Window
    {
        List<Category> categories;
        HomeBudget budget;
        //Takes the list of categories and uses it in the list view
        public CategoriesList(HomeBudget budget)
        {
            this.budget = budget;
            InitializeComponent();
            categories = this.budget.categories.List();
            CategoryList.ItemsSource = categories;
        }

        //Closes this specific window
        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

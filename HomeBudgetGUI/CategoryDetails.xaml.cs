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
using Budget;

namespace HomeBudgetGUI
{
    /// <summary>
    /// Interaction logic for CategoryDetails.xaml
    /// </summary>
    public partial class CategoryDetails : Window
    {
        private HomeBudget budget;
        //Puts the Category types in the combo box
        public CategoryDetails(HomeBudget budget)
        {
            this.budget = budget;
            InitializeComponent();
            CategoryTypes.ItemsSource = Enum.GetValues(typeof(Category.CategoryType));
        }

        //Verifies the inputted data then adds it if it is valid, and it does not exist, and clear the description textbox after entry is successful.
        private void AddCatbtn_Click(object sender, RoutedEventArgs e)
        {
            List<Category> categories = this.budget.categories.List();
            for (int i = 0; i < categories.Count; i++)
            {
                
                if (Convert.ToString(CatDesc.Text) == categories[i].Description)
                {
                    MessageBox.Show("Cannot add Category due to the description name already existing", "Already Exist", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if(Convert.ToString(CatDesc.Text) == "")
            {
                MessageBox.Show("Please enter a description in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (CategoryTypes.SelectedItem == null)
            {
                MessageBox.Show("Please select a category type in order to continue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                this.budget.categories.Add(Convert.ToString(CatDesc.Text), (Category.CategoryType)(CategoryTypes.SelectedIndex)+1);
                MessageBox.Show("Category Added Sucessfully");
                CatDesc.Clear();
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
            
            if (Convert.ToString(CatDesc.Text) != "" || CategoryTypes.SelectedItem != null)
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

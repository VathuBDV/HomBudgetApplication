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
    /// Interaction logic for ExpensesList.xaml
    /// </summary>
    public partial class ExpensesList : Window
    {
        DataPresenter presenter;
        //List<Expense> expenses;
        HomeBudget budget;
        List<Category> categories;
        int searchLastSelected = -1;
        string oldvalue;
        bool somethingFound = false;
        DataGridView view;
        bool allowModify = true;

        
        //Takes the list of expenses and uses it in the list view.
        public ExpensesList(HomeBudget budget)
        {
            this.budget = budget;
            InitializeComponent();
            categories = this.budget.categories.List();
            Categories.DisplayMemberPath = "Description";
            Categories.ItemsSource = categories;
            view = theDataGridView;
            presenter = new DataPresenter(view, budget, this);
            view.presenter = presenter;
            //StartingValues();
            startGet();
        }

        // =============================================================
        // View Interface
        // =============================================================
        /*#region View Interface
        DataPresenter IDataView.presenter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<object> DataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ResetFocusAfterUpdate(int itemIndex)
        {
            throw new NotImplementedException();
        }

        public void DataClear()
        {
            ExpenseList.Columns.Clear();
        }

        public void InitializeStandardDisplay()
        {
            DataGridTextColumn columnExpenseID = new DataGridTextColumn();
            columnExpenseID.Binding = new Binding("ExpenseID");
            columnExpenseID.Header = "Expense ID";
            ExpenseList.Columns.Add(columnExpenseID);

            DataGridTextColumn columnDate = new DataGridTextColumn();
            columnDate.Binding = new Binding("Date");
            columnDate.Header = "Date";
            ExpenseList.Columns.Add(columnDate);

            DataGridTextColumn columnCategory = new DataGridTextColumn();
            columnCategory.Binding = new Binding("Category");
            columnCategory.Header = "Category";
            ExpenseList.Columns.Add(columnCategory);

            DataGridTextColumn columnDesc = new DataGridTextColumn();
            columnDesc.Binding = new Binding("ShortDescription");
            columnDesc.Header = "Description";
            ExpenseList.Columns.Add(columnDesc);

            DataGridTextColumn columnAmount = new DataGridTextColumn();
            columnAmount.Binding = new Binding("Amount");
            columnAmount.Binding.StringFormat = "F2";
            Style style = new Style();
            style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty,
            TextAlignment.Right));
            columnAmount.CellStyle = style;
            columnAmount.Header = "Amount";
            ExpenseList.Columns.Add(columnAmount);

            DataGridTextColumn columnBalance = new DataGridTextColumn();
            columnBalance.Binding = new Binding("Balance");
            columnBalance.Binding.StringFormat = "F2";
            Style style1 = new Style();
            style1.Setters.Add(new Setter(TextBlock.TextAlignmentProperty,
            TextAlignment.Right));
            columnBalance.CellStyle = style1;
            columnBalance.Header = "Balance";
            ExpenseList.Columns.Add(columnBalance);
        }

        public void InitializeByMonthDisplay()
        {
            DataGridTextColumn columnMonth = new DataGridTextColumn();
            columnMonth.Binding = new Binding("Month");
            columnMonth.Header = "Month";
            ExpenseList.Columns.Add(columnMonth);

            DataGridTextColumn columnTotal = new DataGridTextColumn();
            columnTotal.Binding = new Binding("Total");
            columnTotal.Binding.StringFormat = "F2";
            Style style = new Style();
            style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty,
            TextAlignment.Right));
            columnTotal.CellStyle = style;
            columnTotal.Header = "Total";
            ExpenseList.Columns.Add(columnTotal);
        }

        public void InitializeByCategoryDisplay()
        {
            DataGridTextColumn columnCategory = new DataGridTextColumn();
            columnCategory.Binding = new Binding("Category");
            columnCategory.Header = "Category";
            ExpenseList.Columns.Add(columnCategory);

            DataGridTextColumn columnTotal = new DataGridTextColumn();
            columnTotal.Binding = new Binding("Total");
            columnTotal.Binding.StringFormat = "F2";
            Style style = new Style();
            style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty,
            TextAlignment.Right));
            columnTotal.CellStyle = style;
            columnTotal.Header = "Total";
            ExpenseList.Columns.Add(columnTotal);
        }

        public void InitializeByCategoryAndMonthDisplay(List<string> usedCategoryList)
        {
            DataGridTextColumn columnMonth = new DataGridTextColumn();
            columnMonth.Binding = new Binding("[Month]");
            columnMonth.Header = "Month";
            ExpenseList.Columns.Add(columnMonth);

            foreach (Category category in this.budget.categories.List())
            {
                DataGridTextColumn columnCategory = new DataGridTextColumn();
                columnCategory.Binding = new Binding("[" + category.Description + "]");
                columnCategory.Binding.StringFormat = "F2";
                Style style1 = new Style();
                style1.Setters.Add(new Setter(TextBlock.TextAlignmentProperty,
                TextAlignment.Right));
                columnCategory.CellStyle = style1;
                columnCategory.Header = category.Description;
                ExpenseList.Columns.Add(columnCategory);
            }

            DataGridTextColumn columnTotal = new DataGridTextColumn();
            columnTotal.Binding = new Binding("[Total]");
            columnTotal.Binding.StringFormat = "F2";
            Style style = new Style();
            style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty,
            TextAlignment.Right));
            columnTotal.CellStyle = style;
            columnTotal.Header = "Total";
            ExpenseList.Columns.Add(columnTotal);
        }
        #endregion*/


        //Closes this specific window.
        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Add the
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            ExpenseDetails newExpenseDetails = new ExpenseDetails(budget);
            newExpenseDetails.ShowDialog();
            sortBy(sender, e);
        }

        //Clears everything
        private void Resetbtn_Click(object sender, RoutedEventArgs e)
        {
            StartDatePick.SelectedDate = null;
            EndDatePick.SelectedDate = null;
            FilterByCategoryCheck.IsChecked = false;
            Categories.SelectedItem = null;
            FilterByCategory.IsChecked = false;
            FilterByMonth.IsChecked = false;
            sortBy(sender, e);
        }

        //The way it sorts the expenses
        private void sortBy(object sender, RoutedEventArgs e)
        {
            //Only Modify and Delete when individual expenses can be seen.
            if(FilterByCategory.IsChecked == true || FilterByMonth.IsChecked == true)
            {
                view.Updatable.IsEnabled = false;
                view.Updatable1.IsEnabled = false;
                Searchbtn.IsEnabled = false;
                allowModify = false;
                
            }
            else
            {
                view.Updatable.IsEnabled = true;
                view.Updatable1.IsEnabled = true;
                Searchbtn.IsEnabled = true;
                allowModify = true;
            }

            //If the user selected a specific category then this selects it.
            int selectedId = -1;
            if(Categories.SelectedIndex >= 0)
            {
                selectedId = ((Category)Categories.SelectedItem).Id;
            }

            //End date can't be before start date.
            if(StartDatePick.SelectedDate > EndDatePick.SelectedDate)
            {
                EndDatePick.SelectedDate = StartDatePick.SelectedDate;
            }
            
            //If only the Month checkbox is Checked
            if ((bool)FilterByMonth.IsChecked && !(bool)FilterByCategory.IsChecked)
            {
                presenter.showData("Month", StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                //view.ExpenseList.ItemsSource = this.budget.GetBudgetItemsByMonth(StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                
            }
            //If only the category checkbox is checked
            else if (!(bool)FilterByMonth.IsChecked && (bool)FilterByCategory.IsChecked)
            {
                presenter.showData("Category", StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                //List<BudgetItemsByCategory> budgetItemsByCategory = this.budget.GetBudgetItemsByCategory(StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                //view.ExpenseList.ItemsSource = budgetItemsByCategory;
            }
            //If both of the checkboxes are checked
            else if ((bool)FilterByMonth.IsChecked && (bool)FilterByCategory.IsChecked)
            {
                presenter.showData("MonthAndCategory", StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                //List<Dictionary<string, object>> budgetDictionaryByCategoryAndMonth = this.budget.GetBudgetDictionaryByCategoryAndMonth(StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                //view.ExpenseList.ItemsSource = budgetDictionaryByCategoryAndMonth;
            }
            //If none of the checkboxes are checked
            else
            {
                presenter.showData("Standard", StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                //List<BudgetItem> budgetItems = this.budget.GetBudgetItems(StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, selectedId);
                //view.ExpenseList.ItemsSource = budgetItems;
            }
        }

        public void Modify(object sender, RoutedEventArgs e)
        {
            if (allowModify == true)
            { 
                BudgetItem budgetItem = (BudgetItem)view.ExpenseList.SelectedItem;
            ModifyDetails modifyWindow = new ModifyDetails(this.budget, budgetItem.ExpenseID);



            modifyWindow.DatePick.SelectedDate = budgetItem.Date;
            double amount = budgetItem.Amount;
            //Checks if its a negative, and makes it positive
            if (amount < 0)
            {
                amount *= -1;
            }
            modifyWindow.Amount.Text = amount.ToString();
            modifyWindow.Desc.Text = budgetItem.ShortDescription;

            //Selects the category that is was.
            foreach (Category category in Categories.Items)
            {
                if (category.Description == budgetItem.Category)
                {
                    modifyWindow.Categories.SelectedIndex = category.Id - 1;
                    break;
                }
            }

            modifyWindow.ShowDialog();
            sortBy(sender, e);
            }


        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            BudgetItem budgetItem = (BudgetItem)view.ExpenseList.SelectedItem;
            this.budget.expenses.Delete(budgetItem.ExpenseID);
            sortBy(sender, e);
        }

        //I did not want to move search to datagridView controller because it would look like a mess
        private void Searchbtn_Click(object sender, RoutedEventArgs e)
        {
            string value = Searchbox.Text.ToLower();
           
            
            if (value == "")
            {
                MessageBox.Show("Search bar is empty");
            }
            else
            {
                if(value != oldvalue)
                {
                    somethingFound = false;
                }
                if(view.ExpenseList.SelectedIndex != searchLastSelected)
                {
                    searchLastSelected = view.ExpenseList.SelectedIndex;
                }
                for(int i = searchLastSelected+1; i <= view.ExpenseList.Items.Count; i++)
                {
                    if (i == view.ExpenseList.Items.Count)
                    {
                        i = -1;
                        continue;
                    }
                    view.ExpenseList.SelectedIndex = i;
                    BudgetItem item = (BudgetItem)view.ExpenseList.SelectedItem;
                    
                    if (item.ShortDescription.ToLower().Contains(value) || item.Amount.ToString().Contains(value))
                    {
                        somethingFound = true;
                        oldvalue = value;
                        view.ExpenseList.SelectedItem = item;
                        view.ExpenseList.Focus();
                        view.ExpenseList.ScrollIntoView(view.ExpenseList.SelectedItem);
                        searchLastSelected = view.ExpenseList.SelectedIndex;
                        
                        break;
                    }
                    if (somethingFound == false && i == view.ExpenseList.Items.Count-1)
                    {
                        MessageBox.Show("Nothing Found");
                        break;
                    }

                }
                
            } 
        }

        private void startGet()
        {
            presenter.showData("Standard", StartDatePick.SelectedDate, EndDatePick.SelectedDate, (bool)FilterByCategoryCheck.IsChecked, 0);
        }

        
    }

}

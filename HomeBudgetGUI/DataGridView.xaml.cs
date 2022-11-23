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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeBudgetGUI
{
    /// <summary>
    /// Interaction logic for DataGridView.xaml
    /// </summary>
    public partial class DataGridView : UserControl, IDataView
    {
        
        public DataGridView()
        {
            InitializeComponent();
        }

        private DataPresenter seter;
        
        //VIEWS
        #region views
        public DataPresenter presenter { 
            get { return seter; }
            set { seter = value; } 
        }
        public List<object> DataSource {
            get { return ExpenseList.ItemsSource.Cast<object>().ToList(); }
            set { ExpenseList.ItemsSource = value; }
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

            foreach (String category in usedCategoryList)
            {
                DataGridTextColumn columnCategory = new DataGridTextColumn();
                columnCategory.Binding = new Binding("[" + category + "]");
                columnCategory.Binding.StringFormat = "F2";
                Style style1 = new Style();
                style1.Setters.Add(new Setter(TextBlock.TextAlignmentProperty,
                TextAlignment.Right));
                columnCategory.CellStyle = style1;
                columnCategory.Header = category;
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

        public void ResetFocusAfterUpdate(int itemIndex)
        {
            throw new NotImplementedException();
        }
        #endregion


        //Modify the Item.
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            presenter.modifyData(sender, e);
        }

        //Deletes the item
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            presenter.DeleteItem(sender, e);
        }
    }
}

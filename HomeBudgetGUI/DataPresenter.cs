using Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeBudgetGUI
{
    public class DataPresenter
    {
        IDataView gridView;
        HomeBudget budget;
        ExpensesList window;


        public DataPresenter(IDataView view, HomeBudget budget, ExpensesList window)
        {
            gridView = view;
            this.budget = budget;
            this.window = window;
        }

        public void showData(string showThis, DateTime? startDate, DateTime? endDate, bool filterFlag, int selectedId)
        {
            if(showThis == "Category")
            {
                gridView.DataClear();
                gridView.DataSource = this.budget.GetBudgetItemsByCategory(startDate, endDate, filterFlag, selectedId).Cast<object>().ToList();
                gridView.InitializeByCategoryDisplay();
            }
            else if(showThis == "Month")
            {
                gridView.DataClear();
                gridView.DataSource = this.budget.GetBudgetItemsByMonth(startDate, endDate, filterFlag, selectedId).Cast<object>().ToList();
                gridView.InitializeByMonthDisplay();
            }
            else if(showThis == "Standard")
            {
                gridView.DataClear();
                gridView.DataSource = this.budget.GetBudgetItems(startDate, endDate, filterFlag, selectedId).Cast<object>().ToList();
                gridView.InitializeStandardDisplay();
            }
            else if(showThis == "MonthAndCategory")
            {
                gridView.DataClear();
                List<string> list = new List<string>();
                foreach(Category category in this.budget.categories.List())
                {
                    list.Add(category.Description);
                }
                gridView.DataSource = this.budget.GetBudgetDictionaryByCategoryAndMonth(startDate, endDate, filterFlag, selectedId).Cast<object>().ToList();
                
                gridView.InitializeByCategoryAndMonthDisplay(list);
            }
        }

        public void modifyData(object sender, RoutedEventArgs e)
        {
            this.window.Modify(sender, e);
        }

        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            this.window.Delete(sender, e);
        }
        
    }
}

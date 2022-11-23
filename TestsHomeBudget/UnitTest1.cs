using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HomeBudgetGUI;
using System.Collections.Generic;
using Budget;

namespace TestsHomeBudget
{
    public class TestIDataView : IDataView
    {
        public bool dataClear = false;
        public bool categoryDisplay = false;
        public bool monthDisplay = false;
        public bool monthAndCategoryDisplay = false;
        public bool standardDisplay = false;
        public List<object> source;
        public DataPresenter presenter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<object> DataSource {
            get { return source; }
            set { source = value; }
        }

        public void DataClear()
        {
            dataClear = true;
        }

        public void InitializeByCategoryAndMonthDisplay(List<string> usedCategoryList)
        {
            monthAndCategoryDisplay = true;
        }

        public void InitializeByCategoryDisplay()
        {
            categoryDisplay = true;
        }

        public void InitializeByMonthDisplay()
        {
            monthDisplay = true;
        }

        public void InitializeStandardDisplay()
        {
            standardDisplay = true;
        }

        public void ResetFocusAfterUpdate(int itemIndex)
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckStandard()
        {
            HomeBudget budget = new HomeBudget("./test2.db");
            //Arrange
            ExpensesList expensesList = new ExpensesList(budget);
            TestIDataView testIDataView = new TestIDataView();
            DataPresenter theDataPresenter = new DataPresenter(testIDataView, budget, expensesList);

            //Act
            theDataPresenter.showData("Standard", null, null, false, 0);
            

            //Assert
            Assert.IsTrue(testIDataView.standardDisplay);
            Assert.IsTrue(testIDataView.dataClear);
            Assert.AreEqual(testIDataView.DataSource.Count, budget.GetBudgetItems(null, null, false, 0).Count);
        }

        [TestMethod]
        public void CheckMonth()
        {
            HomeBudget budget = new HomeBudget("./test2.db");
            //Arrange
            ExpensesList expensesList = new ExpensesList(budget);
            TestIDataView testIDataView = new TestIDataView();
            DataPresenter theDataPresenter = new DataPresenter(testIDataView, budget, expensesList);

            //Act
            theDataPresenter.showData("Month", null, null, false, 0);


            //Assert
            Assert.IsTrue(testIDataView.monthDisplay);
            Assert.IsTrue(testIDataView.dataClear);
            Assert.AreEqual(testIDataView.DataSource.Count, budget.GetBudgetItemsByMonth(null, null, false, 0).Count);
        }

        [TestMethod]
        public void CheckCategory()
        {
            HomeBudget budget = new HomeBudget("./test2.db");
            //Arrange
            ExpensesList expensesList = new ExpensesList(budget);
            TestIDataView testIDataView = new TestIDataView();
            DataPresenter theDataPresenter = new DataPresenter(testIDataView, budget, expensesList);

            //Act
            theDataPresenter.showData("Category", null, null, false, 0);


            //Assert
            Assert.IsTrue(testIDataView.categoryDisplay);
            Assert.IsTrue(testIDataView.dataClear);
            Assert.AreEqual(testIDataView.DataSource.Count, budget.GetBudgetItemsByCategory(null, null, false, 0).Count);
        }
        [TestMethod]
        public void CheckMonthAndCategory()
        {
            HomeBudget budget = new HomeBudget("./test2.db");
            //Arrange
            ExpensesList expensesList = new ExpensesList(budget);
            TestIDataView testIDataView = new TestIDataView();
            DataPresenter theDataPresenter = new DataPresenter(testIDataView, budget, expensesList);

            //Act
            theDataPresenter.showData("MonthAndCategory", null, null, false, 0);


            //Assert
            Assert.IsTrue(testIDataView.monthAndCategoryDisplay);
            Assert.IsTrue(testIDataView.dataClear);
            Assert.AreEqual(testIDataView.DataSource.Count, budget.GetBudgetDictionaryByCategoryAndMonth(null, null, false, 0).Count);
        }

        /*[TestMethod]
        public void CheckIfAllCanBeCalled()
        {
            HomeBudget budget = new HomeBudget("./test2.db");
            //Arrange
            ExpensesList expensesList = new ExpensesList(budget);
            TestIDataView testIDataView = new TestIDataView();
            DataPresenter theDataPresenter = new DataPresenter(testIDataView, budget,expensesList);

            //Act
            theDataPresenter.showData("Standard", null, null, false, 0);
            theDataPresenter.showData("Month", null, null, false, 0);
            theDataPresenter.showData("Category", null, null, false, 0);
            theDataPresenter.showData("MonthAndCategory", null, null, false, 0);

            //Assert
            Assert.IsTrue(testIDataView.standardDisplay);
            Assert.IsTrue(testIDataView.monthDisplay);
            Assert.IsTrue(testIDataView.categoryDisplay);
            Assert.IsTrue(testIDataView.monthAndCategoryDisplay);
        }

        [TestMethod]
        public void CheckIfColumnsGetCleared()
        {
            HomeBudget budget = new HomeBudget("./test2.db");
            //Arrange
            ExpensesList expensesList = new ExpensesList(budget);
            TestIDataView testIDataView = new TestIDataView();
            DataPresenter theDataPresenter = new DataPresenter(testIDataView, budget, expensesList);

            //Act
            theDataPresenter.showData("Standard", null, null, false, 0);
            theDataPresenter.showData("Month", null, null, false, 0);

            //Assert
            Assert.IsTrue(testIDataView.dataClear);
        }

        [TestMethod]
        public void CheckDataSource()
        {
            HomeBudget budget = new HomeBudget("./test2.db");
            //Arrange
            ExpensesList expensesList = new ExpensesList(budget);
            TestIDataView testIDataView = new TestIDataView();
            DataPresenter theDataPresenter = new DataPresenter(testIDataView, budget, expensesList);

            //Act
            theDataPresenter.showData("Standard", null, null, false, 0);

            //Assert
            Assert.AreEqual(testIDataView.DataSource.Count, budget.expenses.List().Count);
        }*/
    }
}
